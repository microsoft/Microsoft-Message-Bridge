// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeInitializingEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the bridge is initializing.
    /// </summary>
    public class BridgeInitializingEventArgs : BeginEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public MessageBusBridgeDescription Description { get; set; }

        #endregion
    }
}