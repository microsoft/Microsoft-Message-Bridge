// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureEventHubException.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureEventHub
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     A messaging exception specific to Azure.
    /// </summary>
    [Serializable]
    public class AzureEventHubException : MessageBusException
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AzureEventHubException" /> class.
        /// </summary>
        public AzureEventHubException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureEventHubException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AzureEventHubException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureEventHubException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public AzureEventHubException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureEventHubException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected AzureEventHubException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}