// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessagingExceptionEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System;

    /// <summary>
    ///     The data for an <see cref="Exception" /> event.
    /// </summary>
    public class MessagingExceptionEventArgs : EventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }

        #endregion
    }
}