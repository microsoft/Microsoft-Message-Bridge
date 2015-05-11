// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockSubscriber.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     A mock <see cref="ISubscriber" />
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class MockSubscriber : ISubscriber
    {
        #region Public Properties

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public SubscriberDescription Description { get; private set; }

        /// <summary>
        ///     Gets the handler.
        /// </summary>
        /// <value>
        ///     The handler.
        /// </value>
        public Func<IMessage, Task> Handler { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is closed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is closed; otherwise, <c>false</c>.
        /// </value>
        public bool IsClosed { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the subscriber asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public Task CloseAsync()
        {
            return Task.Run(
                () =>
                    {
                        Assert.IsTrue(this.IsInitialized, "The subscriber must be initialized before it is closed.");
                        this.IsClosed = true;
                    });
        }

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
        public Task InitializeAsync(SubscriberDescription description, Func<IMessage, Task> handler)
        {
            return Task.Run(
                () =>
                    {
                        Assert.IsFalse(this.IsClosed, "The subscriber must not be closed before it is initialized.");
                        this.Description = description;
                        this.Handler = handler;
                        this.IsInitialized = true;
                    });
        }

        #endregion
    }
}