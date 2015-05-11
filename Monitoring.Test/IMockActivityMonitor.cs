// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMockActivityMonitor.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Test
{
    using System.Threading.Tasks;

    /// <summary>
    ///     A mock activity monitoring interface for unit testing.
    /// </summary>
    internal interface IMockActivityMonitor : IMessagingActivityMonitor
    {
        #region Public Methods and Operators

        /// <summary>
        /// Called when ending an activity.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The <see cref="MockEndingEventArgs"/> instance containing the event data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>
        /// </returns>
        Task OnEnding(object sender, MockEndingEventArgs args);

        /// <summary>
        /// Called when starting an activity.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The <see cref="MockStartingEventArgs"/> instance containing the event data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>
        /// </returns>
        Task OnStarting(object sender, MockStartingEventArgs args);

        #endregion
    }
}