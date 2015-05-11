// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublisherSendingEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the publisher is sending a message.
    /// </summary>
    public class PublisherSendingEventArgs : BeginEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public IMessage Message { get; set; }

        #endregion
    }
}