// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureEventHubSubscriber.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureEventHub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.ServiceBus.Messaging;

    using Microsoft.MessageBridge.Monitoring;

    /// <summary>
    ///     The azure event hub subscriber.
    /// </summary>
    public class AzureEventHubSubscriber : AzureEventHubClient, ISubscriber, IEventProcessorFactory
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the callback.
        /// </summary>
        private Func<IMessage, Task> Callback { get; set; }

        /// <summary>
        ///     Gets or sets the host.
        /// </summary>
        private EventProcessorHost Host { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the subscriber asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task CloseAsync()
        {
            using (ActivityMonitor.Instance.SubscriberClose(this))
            {
                try
                {
                    await this.Host.UnregisterEventProcessorAsync();
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportSubscriberException(this, e, false);
                    throw new AzureEventHubException(e.Message, e);
                }
            }
        }

        /// <summary>
        /// Creates a new instance of an event processor in the specified partition.
        /// </summary>
        /// <param name="context">
        /// Partition context information.
        /// </param>
        /// <returns>
        /// An instance of <see cref="T:Microsoft.ServiceBus.Messaging.IEventProcessorFactory"/>.
        /// </returns>
        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new AzureEventHubProcessor(this, this.Callback);
        }

        /// <summary>
        /// Initializes the subscriber asynchronously.
        /// </summary>
        /// <param name="description">
        /// The <see cref="SubscriberDescription">description</see>.
        /// </param>
        /// <param name="handler">
        /// The handler callback method.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        public async Task InitializeAsync(SubscriberDescription description, Func<IMessage, Task> handler)
        {
            using (ActivityMonitor.Instance.SubscriberInitialize(this, description))
            {
                try
                {
                    await this.EnsureEventHubExistsAsync(description);
                    await this.EnsureConsumerGroupExistsAsync(description);
                    this.Callback = handler;

                    var name = Guid.NewGuid().ToString("N");
                    var path = description.Entity;
                    var hcs = description.ConnectionString;
                    var scs = description.StorageConnectionString;
                    var group = description.Name;

                    this.Host = new EventProcessorHost(name, path, group, hcs, scs);

                    var options = new EventProcessorOptions();
                    options.ExceptionReceived += this.OnProcessorExceptionReceived;

                    await this.Host.RegisterEventProcessorFactoryAsync(this, options);
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportSubscriberException(this, e, false);
                    throw new AzureEventHubException(e.Message, e);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when a processor exception is received.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The <see cref="ExceptionReceivedEventArgs"/> instance containing the event
        ///     data.
        /// </param>
        private void OnProcessorExceptionReceived(object sender, ExceptionReceivedEventArgs args)
        {
            ActivityMonitor.Instance.ReportSubscriberException(this, args.Exception, false);
        }

        #endregion

        /// <summary>
        ///     The <see cref="IEventProcessor" /> implementation that receives incoming events on the hub partition.
        /// </summary>
        private class AzureEventHubProcessor : IEventProcessor
        {
            #region Fields

            /// <summary>
            ///     The method to be called when a message is received.
            /// </summary>
            private readonly Func<IMessage, Task> callback;

            /// <summary>
            ///     The subscriber.
            /// </summary>
            private readonly ISubscriber subscriber;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="AzureEventHubProcessor"/> class.
            /// </summary>
            /// <param name="subscriber">
            /// The subscriber.
            /// </param>
            /// <param name="handler">
            /// The handler.
            /// </param>
            public AzureEventHubProcessor(ISubscriber subscriber, Func<IMessage, Task> handler)
            {
                this.subscriber = subscriber;
                this.callback = handler;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Called when the ownership of partition moves to a different node for load-balancing purpose, or when the host is
            ///     shutting down. Called in response to
            ///     <see cref="M:Microsoft.ServiceBus.Messaging.EventHubConsumerGroup.UnregisterProcessorAsync(Microsoft.ServiceBus.Messaging.Lease,Microsoft.ServiceBus.Messaging.CloseReason)"/>
            ///     .
            /// </summary>
            /// <param name="context">
            /// Partition ownership information for the partition on which this processor instance works. You can
            ///     call <see cref="M:Microsoft.ServiceBus.Messaging.PartitionContext.CheckpointAsync"/> to checkpoint progress in the
            ///     processing of messages from Event Hub streams.
            /// </param>
            /// <param name="reason">
            /// The reason for calling
            ///     <see cref="M:Microsoft.ServiceBus.Messaging.IEventProcessor.CloseAsync(Microsoft.ServiceBus.Messaging.PartitionContext,Microsoft.ServiceBus.Messaging.CloseReason)"/>
            ///     .
            /// </param>
            /// <returns>
            /// A task indicating that the Close operation is complete.
            /// </returns>
            public Task CloseAsync(PartitionContext context, CloseReason reason)
            {
                return Task.Run(() => { });
            }

            /// <summary>
            /// Initializes the Event Hub processor instance. This method is called before any event data is passed to this
            ///     processor instance.
            /// </summary>
            /// <param name="context">
            /// Ownership information for the partition on which this processor instance works. Any attempt to
            ///     call <see cref="M:Microsoft.ServiceBus.Messaging.PartitionContext.CheckpointAsync"/> will fail during the Open
            ///     operation.
            /// </param>
            /// <returns>
            /// The task that indicates that the Open operation is complete.
            /// </returns>
            public Task OpenAsync(PartitionContext context)
            {
                return Task.Run(() => { });
            }

            /// <summary>
            /// Asynchronously processes the specified context and messages. This method is called when there are new messages in
            ///     the Event Hubs stream. Make sure to checkpoint only when you are finished processing all the events in each batch.
            /// </summary>
            /// <param name="context">
            /// Ownership information for the partition on which this processor instance works.
            /// </param>
            /// <param name="messages">
            /// A batch of Event Hubs events.
            /// </param>
            /// <returns>
            /// The task that indicates that
            ///     <see cref="M:Microsoft.ServiceBus.Messaging.IEventProcessor.ProcessEventsAsync(Microsoft.ServiceBus.Messaging.PartitionContext,System.Collections.Generic.IEnumerable{Microsoft.ServiceBus.Messaging.EventData})"/>
            ///     is complete.
            /// </returns>
            public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
            {
                await Task.WhenAll(messages.Select(m => m.ToMessage()).Select(this.ProcessEventAsync));
                await context.CheckpointAsync();
            }

            #endregion

            #region Methods

            /// <summary>
            /// Processes a single event asynchronously.
            /// </summary>
            /// <param name="message">
            /// The message.
            /// </param>
            /// <returns>
            /// The <see cref="Task"/> that indicates processing the message is complete.
            /// </returns>
            private async Task ProcessEventAsync(IMessage message)
            {
                using (ActivityMonitor.Instance.SubscriberReceive(this.subscriber, message))
                {
                    try
                    {
                        await this.callback(message);
                    }
                    catch (Exception e)
                    {
                        ActivityMonitor.Instance.ReportSubscriberException(this.subscriber, e, false);
                        throw new AzureEventHubException(e.Message, e);
                    }
                }
            }

            #endregion
        }
    }
}