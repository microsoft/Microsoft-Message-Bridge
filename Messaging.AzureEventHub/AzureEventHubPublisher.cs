// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureEventHubPublisher.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureEventHub
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.ServiceBus.Messaging;

    using Microsoft.MessageBridge.Monitoring;

    /// <summary>
    ///     The Azure Event Hub implementation of <see cref="IPublisher" />.
    /// </summary>
    public class AzureEventHubPublisher : AzureEventHubClient, IPublisher
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the <see cref="EventHubClient" /> instance.
        /// </summary>
        protected EventHubClient Client { get; set; }

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
                    throw new AzureEventHubException(e.Message, e);
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
        public async Task InitializeAsync(PublisherDescription description)
        {
            using (ActivityMonitor.Instance.PublisherInitialize(this, description))
            {
                try
                {
                    await this.EnsureEventHubExistsAsync(description);

                    var cs = description.ConnectionString;
                    var hub = description.Entity;

                    this.Client = EventHubClient.CreateFromConnectionString(cs, hub);
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportPublisherException(this, e, false);
                    throw new AzureEventHubException(e.Message, e);
                }
            }
        }

        /// <summary>
        /// Sends a message through the publisher asynchronously.
        /// </summary>
        /// <param name="message">
        /// The <see cref="IMessage">message</see> to be sent.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the send operation.
        /// </returns>
        public async Task SendAsync(IMessage message)
        {
            using (ActivityMonitor.Instance.PublisherSend(this, message))
            {
                try
                {
                    if (this.Client != null)
                    {
                        await this.Client.SendAsync(message.ToEventData());
                    }
                    else
                    {
                        Trace.TraceError(
                            "The message could not be sent because client is null (Message: {0})",
                            message.Message);
                    }
                }
                catch (Exception e)
                {
                    ActivityMonitor.Instance.ReportPublisherException(this, e, false);
                    throw new AzureEventHubException(e.Message, e);
                }
            }
        }

        #endregion
    }
}