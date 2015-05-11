// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureTopicSubscriber.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureServiceBus
{
    using System;
    using System.Linq;
    using System.ServiceModel.Channels;
    using System.Threading.Tasks;
    using System.Timers;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    using Microsoft.MessageBridge.Monitoring;

    using Timer = System.Timers.Timer;

    /// <summary>
    ///     Implements Azure service bus topic subscriber.
    /// </summary>
    public class AzureTopicSubscriber : AzureTopicClient, ISubscriber
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the callback.
        /// </summary>
        private Func<IMessage, Task> Callback { get; set; }

        /// <summary>
        ///     Gets or sets the client.
        /// </summary>
        private SubscriptionClient Client { get; set; }

        /// <summary>
        /// Gets or sets the subscriber description.
        /// </summary>
        /// <value>
        /// The subscriber description.
        /// </value>
        private SubscriberDescription SubscriberDescription { get; set; }

        /// <summary>
        /// Gets or sets the manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        private NamespaceManager Manager { get; set; }

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
                    await this.Client.CloseAsync();
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportSubscriberException(this, e, false);
                    throw new AzureServiceBusException(e.Message, e);
                }
            }
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
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when either argument is null.
        /// </exception>
        public async Task InitializeAsync(SubscriberDescription description, Func<IMessage, Task> handler)
        {
            using (ActivityMonitor.Instance.SubscriberInitialize(this, description))
            {
                if (description == null)
                {
                    var exception = new ArgumentNullException("description");
                    ActivityMonitor.Instance.ReportSubscriberException(this, exception, false);
                    throw exception;
                }

                if (handler == null)
                {
                    var exception = new ArgumentNullException("handler");
                    ActivityMonitor.Instance.ReportSubscriberException(this, exception, false);
                    throw exception;
                }

                try
                {
                    await this.EnsureSubscriptionExistsAsync(description);
                    this.InitializeClient(description, handler);
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportSubscriberException(this, e, false);
                    throw new AzureServiceBusException(e.Message, e);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ensures the subscription exists.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task EnsureSubscriptionExistsAsync(SubscriberDescription description)
        {
            await this.EnsureTopicExistsAsync(description);

            var connectionString = description.ConnectionString;
            var path = description.Entity;
            var name = description.Name;

            this.Manager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!await this.Manager.SubscriptionExistsAsync(path, name))
            {
                await
                    this.Manager.CreateSubscriptionAsync(
                        new SubscriptionDescription(path, name)
                            {
                                EnableBatchedOperations = true,
                                EnableDeadLetteringOnFilterEvaluationExceptions = false,
                                EnableDeadLetteringOnMessageExpiration = false
                            });
            }

            var timer = new Timer { Interval = 60000, Enabled = true };
            timer.Elapsed += this.ReportSubscriberTopicQueueDepth;
        }

        /// <summary>
        /// Initializes the subscription client.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        private void InitializeClient(SubscriberDescription description, Func<IMessage, Task> callback)
        {
            this.SubscriberDescription = description;
            var connectionString = this.SubscriberDescription.ConnectionString;
            var path = this.SubscriberDescription.Entity;
            var name = this.SubscriberDescription.Name;

            this.Callback = callback;

            this.Client = SubscriptionClient.CreateFromConnectionString(
                connectionString,
                path,
                name,
                ReceiveMode.ReceiveAndDelete);

            this.ReceiveMessages();
        }

        /// <summary>
        /// Receives the messages.
        /// </summary>
        private async void ReceiveMessages()
        {
            while (true)
            {
                try
                {
                    var messages = await this.Client.ReceiveBatchAsync(100);

                    foreach (var message in messages)
                    {
                        await this.OnMessageArrivedAsync(message);
                    }
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportSubscriberException(this, e, false);
                }
            }

            // ReSharper disable once FunctionNeverReturns
            // This method must never return. It is the task that pulls messages from the service bus.
        }

        /// <summary>
        /// Called when a brokered message arrives.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <remarks>
        /// Exceptions in this method should not rethrow an exception since this is an event handler and will not be caught
        ///     properly.
        /// </remarks>
        /// <returns>
        /// A <see cref="Task"/> representing the operation.
        /// </returns>
        private async Task OnMessageArrivedAsync(BrokeredMessage message)
        {
            var m = message.ToMessage();

            using (ActivityMonitor.Instance.SubscriberReceive(this, m))
            {
                try
                {
                    await this.Callback(m);
                }
                catch (Exception e1)
                {
                    ActivityMonitor.Instance.ReportSubscriberException(this, e1, false);
                }
            }
        }

        /// <summary>
        /// Reports the subscriber topic queue depth.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void ReportSubscriberTopicQueueDepth(object sender, ElapsedEventArgs args)
        {
            var subscriptionDescription = this.Manager.GetSubscription(this.SubscriberDescription.Entity, this.SubscriberDescription.Name);

            ActivityMonitor.Instance.ReportSubscriptionMessageCount(
                this,
                args,
                subscriptionDescription.MessageCount,
                subscriptionDescription.MessageCountDetails.ActiveMessageCount,
                subscriptionDescription.MessageCountDetails.DeadLetterMessageCount,
                this.SubscriberDescription);
        }

        #endregion
    }
}