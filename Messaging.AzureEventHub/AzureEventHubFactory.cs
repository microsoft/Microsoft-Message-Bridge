// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureEventHubFactory.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureEventHub
{
    /// <summary>
    ///     The <see cref="IMessageBusFactory" /> implementation for Azure Event Hub messaging.
    /// </summary>
    public class AzureEventHubFactory : IMessageBusFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a concrete <see cref="IPublisher" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="IPublisher" /> instance
        /// </returns>
        public IPublisher CreatePublisher()
        {
            return new AzureEventHubPublisher();
        }

        /// <summary>
        ///     Creates a concrete <see cref="ISubscriber" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISubscriber" /> instance
        /// </returns>
        public ISubscriber CreateSubscriber()
        {
            return new AzureEventHubSubscriber();
        }

        #endregion
    }
}