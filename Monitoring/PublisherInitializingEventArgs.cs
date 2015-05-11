// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublisherInitializingEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The data for the event where the publisher is initializing.
    /// </summary>
    public class PublisherInitializingEventArgs : BeginEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public PublisherDescription Description { get; set; }

        #endregion
    }
}