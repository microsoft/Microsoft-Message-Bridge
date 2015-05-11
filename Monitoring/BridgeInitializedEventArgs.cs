// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeInitializedEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the bridge has initialized.
    /// </summary>
    public class BridgeInitializedEventArgs : EndEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public MessageBusBridgeDescription Description { get; set; }

        #endregion
    }
}