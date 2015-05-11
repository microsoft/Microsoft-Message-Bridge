// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityTracker.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     The <see cref="IDisposable" /> activity tracker for capturing the length of an activity in the system.
    /// </summary>
    internal class ActivityTracker : IDisposable
    {
        #region Fields

        /// <summary>
        ///     The callback method.
        /// </summary>
        private readonly Action<TimeSpan> callback;

        /// <summary>
        ///     The activity timer
        /// </summary>
        private readonly Stopwatch timer;

        /// <summary>
        ///     Indicates whether this instance has been disposed.
        /// </summary>
        private bool disposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTracker"/> class.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        public ActivityTracker(Action<TimeSpan> callback)
        {
            this.callback = callback;
            this.timer = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Called when the activity has been completed.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Stops the timer and invokes the callback when the operation has completed.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> stop the operation; <c>false</c> performs no action.
        /// </param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.disposed = true;
                this.timer.Stop();
                this.callback(this.timer.Elapsed);
            }
        }

        #endregion
    }
}