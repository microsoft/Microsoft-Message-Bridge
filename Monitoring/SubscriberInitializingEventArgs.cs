// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberInitializingEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the subscriber is initializing.
    /// </summary>
    public class SubscriberInitializingEventArgs : BeginEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public SubscriberDescription Description { get; set; }

        #endregion
    }
}