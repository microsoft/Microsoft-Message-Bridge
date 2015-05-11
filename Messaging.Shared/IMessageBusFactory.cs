// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageBusFactory.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    /// <summary>
    ///     The message bus factory is used to create <see cref="IPublisher" /> and <see cref="ISubscriber" /> instances for a
    ///     concrete message bus implementation.
    /// </summary>
    public interface IMessageBusFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a concrete <see cref="IPublisher" /> instance.
        /// </summary>
        /// <returns>The <see cref="IPublisher" /> instance</returns>
        IPublisher CreatePublisher();

        /// <summary>
        ///     Creates a concrete <see cref="ISubscriber" /> instance.
        /// </summary>
        /// <returns>The <see cref="ISubscriber" /> instance</returns>
        ISubscriber CreateSubscriber();

        #endregion
    }
}