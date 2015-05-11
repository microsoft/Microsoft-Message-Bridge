// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRFactory.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.MessageBridge.Messaging.SignalR
{
    /// <summary>
    ///     Implements the message bus factory for ASP.NET SignalR hubs.
    /// </summary>
    public class SignalRFactory : IMessageBusFactory
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
            return new SignalRPublisher();
        }

        /// <summary>
        ///     Creates a concrete <see cref="ISubscriber" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISubscriber" /> instance
        /// </returns>
        public ISubscriber CreateSubscriber()
        {
            return new SignalRSubscriber();
        }

        #endregion
    }
}