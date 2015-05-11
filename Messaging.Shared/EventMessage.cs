// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventMessage.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System.Collections.Generic;

    /// <summary>
    ///     A generic implementation of <see cref="IMessage" />.
    /// </summary>
    public class EventMessage : IMessage
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventMessage" /> class.
        /// </summary>
        public EventMessage()
        {
            this.Properties = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the correlation key. The correlation key should be the <see cref="MessageKey" /> of a previous
        ///     message.
        /// </summary>
        public string CorrelationKey { get; set; }

        /// <summary>
        ///     Gets or sets the message. The message is a JSON-encoded object being sent over the message bus.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the message key associated with the message.
        /// </summary>
        public string MessageKey { get; set; }

        /// <summary>
        ///     Gets or sets the partition key associated with the message.
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        ///     Gets the custom properties associated with the message.
        /// </summary>
        public IDictionary<string, object> Properties { get; private set; }

        #endregion
    }
}