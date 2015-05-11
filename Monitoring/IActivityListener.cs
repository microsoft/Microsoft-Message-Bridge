// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IActivityListener.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   The ActivityListener interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    /// <summary>
    ///     The ActivityListener interface.
    /// </summary>
    public interface IActivityListener
    {
        #region Public Methods and Operators

        /// <summary>
        /// Initializes the listener.
        /// </summary>
        /// <param name="monitor">
        /// The monitor.
        /// </param>
        void Initialize(IActivityMonitor monitor);

        #endregion
    }
}