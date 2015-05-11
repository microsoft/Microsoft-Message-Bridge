// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPublisher.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System.Threading.Tasks;

    /// <summary>
    ///     The publisher interface describes the methods on a publisher. A publisher is used for one-way, fire-and-forget
    ///     message communication.
    /// </summary>
    public interface IPublisher
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Closes the publisher instance asynchronously.
        /// </summary>
        /// <returns>The <see cref="Task" /> representing the close operation.</returns>
        Task CloseAsync();

        /// <summary>
        /// Initializes the publisher asynchronously.
        /// </summary>
        /// <param name="description">
        /// The <see cref="PublisherDescription">publisher description</see>.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        Task InitializeAsync(PublisherDescription description);

        /// <summary>
        /// Sends a message through the publisher asynchronously.
        /// </summary>
        /// <param name="message">
        /// The <see cref="IMessage">message</see> to be sent.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the send operation.
        /// </returns>
        Task SendAsync(IMessage message);

        #endregion
    }
}