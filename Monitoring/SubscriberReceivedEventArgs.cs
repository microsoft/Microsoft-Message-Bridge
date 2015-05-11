// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberReceivedEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the subscriber received a message.
    /// </summary>
    public class SubscriberReceivedEventArgs : EndEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public IMessage Message { get; set; }

        #endregion
    }
}