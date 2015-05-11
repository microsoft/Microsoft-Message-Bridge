// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageBusBridge.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Describes the message bus bridge adapter interface.
    /// </summary>
    public interface IMessageBusBridge
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Closes the message bridge and the underlying message bus instances asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the close operation.</returns>
        Task CloseAsync();

        /// <summary>
        /// Initializes the message bus bridge connection asynchronously.
        /// </summary>
        /// <param name="description">
        /// The message bus bridge description.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        Task InitializeAsync(MessageBusBridgeDescription description);

        #endregion
    }
}