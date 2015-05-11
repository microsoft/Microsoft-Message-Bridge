// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberMessageCountEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   The subscriber message count args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.MessageBridge.Monitoring
{
    using System;

    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    /// The subscriber message count args.
    /// </summary>
    public class SubscriberMessageCountEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets or sets the subscriber description.
        /// </summary>
        /// <value>
        /// The subscriber description.
        /// </value>
        public SubscriberDescription SubscriberDescription { get; set; }

        /// <summary>
        /// Gets or sets the total message count.
        /// </summary>
        /// <value>
        /// The total message count.
        /// </value>
        public long TotalMessageCount { get; set; }

        /// <summary>
        /// Gets or sets the active message count.
        /// </summary>
        /// <value>
        /// The active message count.
        /// </value>
        public long ActiveMessageCount { get; set; }

        /// <summary>
        /// Gets or sets the dead letter message count.
        /// </summary>
        /// <value>
        /// The dead letter message count.
        /// </value>
        public long DeadLetterMessageCount { get; set; }

        /// <summary>
        /// Gets or sets the signal time.
        /// </summary>
        public DateTime SignalTime { get; set; }

        #endregion
    }
}