// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessagingActivityMonitor.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System.Threading.Tasks;

    /// <summary>
    ///     The base messaging activity monitor interface.
    /// </summary>
    public interface IMessagingActivityMonitor
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The initialize.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task Initialize();

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The <see cref="MessagingExceptionEventArgs"/> instance containing the event data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> for the operation.
        /// </returns>
        Task OnException(object sender, MessagingExceptionEventArgs args);

        #endregion
    }
}