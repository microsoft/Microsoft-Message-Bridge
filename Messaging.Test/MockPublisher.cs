// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockPublisher.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     A mock <see cref="IPublisher" />
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class MockPublisher : IPublisher
    {
        #region Public Properties

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public PublisherDescription Description { get; private set; }

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

        /// <summary>
        ///     Gets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public IMessage Message { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the publisher instance asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" /> representing the close operation.
        /// </returns>
        public Task CloseAsync()
        {
            return Task.Run(
                () =>
                    {
                        Assert.IsTrue(this.IsInitialized, "The publisher must be initialized before it is closed.");
                        this.IsClosed = true;
                    });
        }

        /// <summary>
        /// Initializes the publisher asynchronously.
        /// </summary>
        /// <param name="description">
        /// The <see cref="PublisherDescription">publisher description</see>.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        public Task InitializeAsync(PublisherDescription description)
        {
            return Task.Run(
                () =>
                    {
                        Assert.IsFalse(this.IsClosed, "The publisher must not be closed when initialized.");
                        this.Description = description;
                        this.IsInitialized = true;
                    });
        }

        /// <summary>
        /// Sends a message through the publisher asynchronously.
        /// </summary>
        /// <param name="message">
        /// The <see cref="IMessage">message</see> to be sent.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the send operation.
        /// </returns>
        public Task SendAsync(IMessage message)
        {
            return Task.Run(() => { this.Message = message; });
        }

        #endregion
    }
}