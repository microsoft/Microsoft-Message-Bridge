// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System.Collections.Generic;

    /// <summary>
    ///     The message interface for communicating on the message bus.
    /// </summary>
    public interface IMessage
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the correlation key. The correlation key should be the <see cref="MessageKey" /> of a previous
        ///     message.
        /// </summary>
        string CorrelationKey { get; set; }

        /// <summary>
        ///     Gets or sets the message. The message is a JSON-encoded object being sent over the message bus.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     Gets or sets the message key associated with the message.
        /// </summary>
        string MessageKey { get; set; }

        /// <summary>
        ///     Gets or sets the partition key associated with the message.
        /// </summary>
        string PartitionKey { get; set; }

        /// <summary>
        ///     Gets the custom properties associated with the message.
        /// </summary>
        IDictionary<string, object> Properties { get; }

        #endregion
    }
}