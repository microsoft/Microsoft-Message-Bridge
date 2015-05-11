// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureServiceBusException.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureServiceBus
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     A messaging exception specific to Azure.
    /// </summary>
    [Serializable]
    public class AzureServiceBusException : MessageBusException
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AzureServiceBusException" /> class.
        /// </summary>
        public AzureServiceBusException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AzureServiceBusException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public AzureServiceBusException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected AzureServiceBusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}