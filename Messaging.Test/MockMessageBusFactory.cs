// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockMessageBusFactory.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     A mock <see cref="IMessageBusFactory" />
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class MockMessageBusFactory : IMessageBusFactory
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the publisher.
        /// </summary>
        /// <value>
        ///     The publisher.
        /// </value>
        public MockPublisher Publisher { get; set; }

        /// <summary>
        ///     Gets or sets the subscriber.
        /// </summary>
        /// <value>
        ///     The subscriber.
        /// </value>
        public MockSubscriber Subscriber { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Creates a concrete <see cref="IPublisher" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="IPublisher" /> instance
        /// </returns>
        public IPublisher CreatePublisher()
        {
            return this.Publisher;
        }

        /// <summary>
        ///     Creates a concrete <see cref="ISubscriber" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISubscriber" /> instance
        /// </returns>
        public ISubscriber CreateSubscriber()
        {
            return this.Subscriber;
        }

        #endregion
    }
}