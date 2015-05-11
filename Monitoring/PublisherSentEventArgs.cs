// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublisherSentEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the publisher has sent a message.
    /// </summary>
    public class PublisherSentEventArgs : EndEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public IMessage Message { get; set; }

        #endregion
    }
}