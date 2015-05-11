// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureTopicFactory.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureServiceBus
{
    /// <summary>
    ///     Implements an Azure service bus topic factory.
    /// </summary>
    public class AzureTopicFactory : IMessageBusFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates the topic publisher.
        /// </summary>
        /// <returns>
        ///     The <see cref="AzureTopicPublisher" />
        /// </returns>
        public IPublisher CreatePublisher()
        {
            return new AzureTopicPublisher();
        }

        /// <summary>
        ///     Creates the subscriber.
        /// </summary>
        /// <returns>
        ///     The <see cref="AzureTopicSubscriber" />
        /// </returns>
        public ISubscriber CreateSubscriber()
        {
            return new AzureTopicSubscriber();
        }

        #endregion
    }
}