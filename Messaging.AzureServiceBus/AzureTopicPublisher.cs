// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureTopicPublisher.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureServiceBus
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.ServiceBus.Messaging;

    using Microsoft.MessageBridge.Monitoring;

    /// <summary>
    ///     Implements an Azure service bus topic publisher.
    /// </summary>
    public class AzureTopicPublisher : AzureTopicClient, IPublisher
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the topic client.
        /// </summary>
        private TopicClient Client { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the publisher instance asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" /> representing the close operation.
        /// </returns>
        public async Task CloseAsync()
        {
            using (ActivityMonitor.Instance.PublisherClose(this))
            {
                try
                {
                    await this.Client.CloseAsync();
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportPublisherException(this, e, false);
                    throw new AzureServiceBusException(e.Message, e);
                }
            }
        }

        /// <summary>
        /// Initializes the publisher asynchronously.
        /// </summary>
        /// <param name="description">
        /// The <see cref="PublisherDescription">publisher description</see>.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when the description is null.
        /// </exception>
        public async Task InitializeAsync(PublisherDescription description)
        {
            using (ActivityMonitor.Instance.PublisherInitialize(this, description))
            {
                if (description == null)
                {
                    var exception = new ArgumentNullException("description");
                    ActivityMonitor.Instance.ReportPublisherException(this, exception, false);
                    throw exception;
                }

                try
                {
                    await this.EnsureTopicExistsAsync(description);
                    this.InitializeClient(description);
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportPublisherException(this, e, false);
                    throw new AzureServiceBusException(e.Message, e);
                }
            }
        }

        /// <summary>
        /// Sends a message asynchronously.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> representing the operation.
        /// </returns>
        public async Task SendAsync(IMessage message)
        {
            using (ActivityMonitor.Instance.PublisherSend(this, message))
            {
                try
                {
                    await this.Client.SendAsync(message.ToBrokeredMessage());
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportPublisherException(this, e, false);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the topic client.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        private void InitializeClient(DescriptionBase description)
        {
            var connectionString = description.ConnectionString;
            var path = description.Entity;
            this.Client = TopicClient.CreateFromConnectionString(connectionString, path);
        }

        #endregion
    }
}