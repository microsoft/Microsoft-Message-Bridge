// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureEventHubClient.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureEventHub
{
    using System.Threading.Tasks;

    using Microsoft.ServiceBus;

    /// <summary>
    ///     The Azure Event Hub Client base class.
    /// </summary>
    public abstract class AzureEventHubClient
    {
        #region Methods

        /// <summary>
        /// Ensures the consumer group exists asynchronously.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="AzureEventHubException">
        /// Occurs if there is an error creating the event hub.
        /// </exception>
        protected async Task EnsureConsumerGroupExistsAsync(SubscriberDescription description)
        {
            var cs = description.ConnectionString;
            var hub = description.Entity;

            var manager = NamespaceManager.CreateFromConnectionString(cs);
            var eventhubdescription = await manager.CreateEventHubIfNotExistsAsync(hub);
            await manager.CreateConsumerGroupIfNotExistsAsync(eventhubdescription.Path, description.Name);
        }

        /// <summary>
        /// Ensures the event hub exists asynchronously.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="AzureEventHubException">
        /// Occurs if there is an error creating the event hub.
        /// </exception>
        protected async Task EnsureEventHubExistsAsync(DescriptionBase description)
        {
            var cs = description.ConnectionString;
            var hub = description.Entity;

            var manager = NamespaceManager.CreateFromConnectionString(cs);
            await manager.CreateEventHubIfNotExistsAsync(hub);
        }

        #endregion
    }
}