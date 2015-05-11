// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System;

    /// <summary>
    ///     The data for an ending event.
    /// </summary>
    public abstract class EndEventArgs : EventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the elapsed time for the operation that is ending.
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        #endregion
    }
}