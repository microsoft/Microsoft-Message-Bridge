// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeTransferredEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the bridge has transferred a message.
    /// </summary>
    public class BridgeTransferredEventArgs : EndEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public IMessage Message { get; set; }

        #endregion
    }
}