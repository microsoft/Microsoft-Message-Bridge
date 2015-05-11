// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockMessageBus.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The mock <see cref="IMessageBus" /> implementation.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class MockMessageBus : IMessageBus
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockMessageBus" /> class.
        /// </summary>
        public MockMessageBus()
        {
            this.Handlers = new ConcurrentDictionary<string, Func<IMessage, Task>>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the handlers.
        /// </summary>
        /// <value>
        ///     The handlers.
        /// </value>
        private ConcurrentDictionary<string, Func<IMessage, Task>> Handlers { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the message bus instance asynchronously.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> representing the close operation.
        /// </returns>
        public Task CloseAsync()
        {
            return Task.Run(() => { });
        }

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
        public Task RegisterHandlerAsync(string entity, string name, Func<IMessage, Task> handler)
        {
            return
                Task.Run(() => Assert.IsTrue(this.Handlers.TryAdd(entity, handler), "The handler could not be added."));
        }

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
        public async Task SendAsync(string entity, IMessage message)
        {
            await this.Handlers[entity].Invoke(message);
        }

        #endregion
    }
}