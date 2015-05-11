// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRSubscriber.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.MessageBridge.Messaging.SignalR
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNet.SignalR.Client;

    /// <summary>
    ///     Implements the subscriber interface for ASP.NET SignalR hubs.
    /// </summary>
    public class SignalRSubscriber : ISubscriber
    {
        #region Fields

        /// <summary>
        ///     The callback method.
        /// </summary>
        private Func<IMessage, Task> callback;

        /// <summary>
        ///     The SignalR hub connection instance.
        /// </summary>
        private HubConnection hub;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the subscriber asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task CloseAsync()
        {
            await Task.Run(() => this.hub.Stop());
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
        public async Task InitializeAsync(SubscriberDescription description, Func<IMessage, Task> handler)
        {
            this.callback = handler;

            // TODO: We should consider adding query string options.
            // TODO: We should consider adding certificate and/or credential options.
            this.hub = new HubConnection(description.ConnectionString);
            this.hub.Received += this.OnReceived;
            await this.hub.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when a message has been received.
        /// </summary>
        /// <param name="s">
        /// The <see cref="string"/> message data from the hub.
        /// </param>
        private void OnReceived(string s)
        {
            var data = new EventMessage { Message = s };
            this.callback(data);
        }

        #endregion
    }
}