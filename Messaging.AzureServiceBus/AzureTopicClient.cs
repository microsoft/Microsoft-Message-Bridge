// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureTopicClient.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureServiceBus
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    ///     The Azure topic client base class.
    /// </summary>
    public class AzureTopicClient
    {
        #region Methods

        /// <summary>
        /// Ensures the topic exists.
        /// </summary>
        /// <param name="description">
        /// The publisher description
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> representing the operation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when the description is null.
        /// </exception>
        /// <exception cref="AzureServiceBusException">
        /// Occurs when a service bus cannot be checked or created.
        /// </exception>
        protected async Task EnsureTopicExistsAsync(DescriptionBase description)
        {
            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            var connectionString = description.ConnectionString;
            var path = description.Entity;

            var manager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!await manager.TopicExistsAsync(path))
            {
                await
                    manager.CreateTopicAsync(
                        new TopicDescription(path) { EnablePartitioning = true, EnableBatchedOperations = true });
            }
        }

        #endregion
    }
}