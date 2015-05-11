// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBus.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    using Microsoft.MessageBridge.Monitoring;

    /// <summary>
    ///     <para>
    ///         A message bus implementation for sending and receiving messages.
    ///     </para>
    ///     <para>
    ///         A message bus is a facility that provides entities for publishing and subscribing to messages. A message bus
    ///         entity directs an homogenous set of message data. It is not required that all messages contain the identical
    ///         properties, but they represent the same object. This means that property A if present has the same meaning as
    ///         property A in all other messages on the entity. A message bus entity is referred to by name. It is equivalent
    ///         to a queue, topic, or hub in most systems.
    ///     </para>
    ///     <para>
    ///         Messages include a JSON-encoded message body along with an optional set of properties, a partition key, and
    ///         a message instance key.
    ///     </para>
    /// </summary>
    public class MessageBus : IMessageBus
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBus"/> class.
        /// </summary>
        /// <param name="description">
        /// The message bus description.
        /// </param>
        public MessageBus(MessageBusDescription description)
        {
            this.Description = description;
            this.Publishers = new ConcurrentDictionary<string, IPublisher>();
            this.Subscribers = new ConcurrentDictionary<string, ISubscriber>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the message bus description. This is used for connecting to the bus.
        /// </summary>
        private MessageBusDescription Description { get; set; }

        /// <summary>
        ///     Gets or sets the publishers. There will be only one publisher per entity per bus instance.
        /// </summary>
        private ConcurrentDictionary<string, IPublisher> Publishers { get; set; }

        /// <summary>
        ///     Gets or sets the subscribers. There will be only one subscription per entity per bus instance.
        /// </summary>
        private ConcurrentDictionary<string, ISubscriber> Subscribers { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the message bus instance asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> representing the close operation.
        /// </returns>
        /// <exception cref="MessageBusException">Occurs when either a publisher or subscriber fails to close properly.</exception>
        public async Task CloseAsync()
        {
            using (ActivityMonitor.Instance.MessageBusClose(this))
            {
                try
                {
                    await Task.WhenAll(this.CloseSubscribersAsync(), this.ClosePublishersAsync());
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportMessageBusException(this, e, false);
                    throw new MessageBusException(e.Message, e);
                }
            }
        }

        /// <summary>
        /// Registers the message bus subscription handler asynchronously.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="handler">
        /// The handler.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when the handler method is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Occurs when the message bus entity name is null or whitespace.
        /// </exception>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task RegisterHandlerAsync(string entity, string name, Func<IMessage, Task> handler)
        {
            using (ActivityMonitor.Instance.MessageBusRegisterHandler(this, entity, name))
            {
                if (string.IsNullOrWhiteSpace(entity))
                {
                    var exception = new ArgumentException("The entity name cannot be null or empty.", entity);
                    ActivityMonitor.Instance.ReportMessageBusException(this, exception, false);
                    throw exception;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    var exception = new ArgumentException("The subscription name cannot be null or empty.", name);
                    ActivityMonitor.Instance.ReportMessageBusException(this, exception, false);
                    throw exception;
                }

                if (handler == null)
                {
                    var exception = new ArgumentNullException("handler");
                    ActivityMonitor.Instance.ReportMessageBusException(this, exception, false);
                    throw exception;
                }

                var subscriber = this.Description.Factory.CreateSubscriber();

                if (this.Subscribers.TryAdd(entity, subscriber))
                {
                    var cs = this.Description.ConnectionString;

                    try
                    {
                        await
                            subscriber.InitializeAsync(
                                new SubscriberDescription
                                    {
                                        ConnectionString = cs, 
                                        Entity = entity, 
                                        Name = name, 
                                        StorageConnectionString =
                                            this.Description.StorageConnectionString
                                    }, 
                                handler);
                    }
                    catch (Exception e)
                    {
                        ActivityMonitor.Instance.ReportMessageBusException(this, e, false);
                        throw new MessageBusException(e.Message, e);
                    }
                }
            }
        }

        /// <summary>
        /// Sends the message to the message bus entity asynchronously.
        /// </summary>
        /// <param name="entity">
        /// The message bus entity.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> representing the send operation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when the message argument is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Occurs when the message bus entity name is null or whitespace.
        /// </exception>
        /// <exception cref="MessageBusException">
        /// Occurs when either the publisher cannot be initialized or the publisher fails to
        ///     send the message.
        /// </exception>
        public async Task SendAsync(string entity, IMessage message)
        {
            using (ActivityMonitor.Instance.MessageBusSend(this, message, entity))
            {
                if (string.IsNullOrWhiteSpace(entity))
                {
                    var exception = new ArgumentException("The entity name cannot be null or empty.", entity);
                    ActivityMonitor.Instance.ReportMessageBusException(this, exception, false);
                    throw exception;
                }

                if (message == null)
                {
                    var exception = new ArgumentNullException("message");
                    ActivityMonitor.Instance.ReportMessageBusException(this, exception, false);
                    throw exception;
                }

                try
                {
                    var publisher = this.GetPublisher(entity);
                    await publisher.SendAsync(message);
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportMessageBusException(this, e, false);
                    throw new MessageBusException(e.Message, e);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Closes the publishers asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the close operation.</returns>
        private async Task ClosePublishersAsync()
        {
            foreach (var publishers in this.Publishers.Values)
            {
                await publishers.CloseAsync();
            }

            this.Publishers.Clear();
        }

        /// <summary>
        ///     Closes the subscribers asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the close operation.</returns>
        private async Task CloseSubscribersAsync()
        {
            foreach (var subscriber in this.Subscribers.Values)
            {
                await subscriber.CloseAsync();
            }

            this.Subscribers.Clear();
        }

        /// <summary>
        /// Gets the publisher for the message bus entity asynchronously.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IPublisher"/>
        /// </returns>
        private IPublisher GetPublisher(string entity)
        {
            return this.Publishers.GetOrAdd(
                entity, 
                s =>
                    {
                        var publisher = this.Description.Factory.CreatePublisher();
                        var cs = this.Description.ConnectionString;
                        var cert = this.Description.Certificate;

                        var description = new PublisherDescription
                                               {
                                                   ConnectionString = cs,
                                                   Certificate = cert,
                                                   Entity = entity
                                               };
                        publisher.InitializeAsync(description).Wait();
                        return publisher;
                    });
        }

        #endregion
    }
}