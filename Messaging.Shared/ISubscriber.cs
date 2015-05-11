// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISubscriber.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     The subscriber interface describes method on a subscriber. A subscriber is a message listener. The listener only
    ///     has a single callback method. This means that any filtering must be performed by the listener.
    /// </summary>
    public interface ISubscriber
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Closes the subscriber asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task CloseAsync();

        /// <summary>
        /// Initializes the subscriber asynchronously.
        /// </summary>
        /// <param name="description">
        /// The <see cref="SubscriberDescription">description</see>.
        /// </param>
        /// <param name="handler">
        /// The handler callback method.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        Task InitializeAsync(SubscriberDescription description, Func<IMessage, Task> handler);

        #endregion
    }
}