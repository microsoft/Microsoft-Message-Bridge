// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageBus.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     The message bus interface.
    /// </summary>
    public interface IMessageBus
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Closes the message bus instance asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> representing the close operation.
        /// </returns>
        Task CloseAsync();

        /// <summary>
        /// Registers the message bus subscription handler asynchronously.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="handler">
        /// The handler.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task RegisterHandlerAsync(string entity, string name, Func<IMessage, Task> handler);

        /// <summary>
        /// Sends the message to the message bus entity asynchronously.
        /// </summary>
        /// <param name="entity">
        /// The message bus entity.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> representing the send operation.
        /// </returns>
        Task SendAsync(string entity, IMessage message);

        #endregion
    }
}