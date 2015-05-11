// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebFactory.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Web
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Implements the message bus factory interface for HTTP web calls.
    /// </summary>
    public class WebFactory : IMessageBusFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a concrete <see cref="IPublisher" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="IPublisher" /> instance
        /// </returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "The web publisher will be disposed by the caller.")]
        public IPublisher CreatePublisher()
        {
            return new WebPublisher();
        }

        /// <summary>
        ///     Creates a concrete <see cref="ISubscriber" /> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISubscriber" /> instance
        /// </returns>
        /// <exception cref="System.NotSupportedException">A HTTP web subscriber is not supported at this time.</exception>
        public ISubscriber CreateSubscriber()
        {
            throw new NotSupportedException("A HTTP web subscriber is not supported at this time.");
        }

        #endregion
    }
}