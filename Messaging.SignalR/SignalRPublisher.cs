// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRPublisher.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.SignalR
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using Microsoft.AspNet.SignalR.Client;

    /// <summary>
    ///     Implements the publisher interface for ASP.NET SignalR hubs.
    /// </summary>
    public class SignalRPublisher : IPublisher
    {
        #region Fields

        /// <summary>
        ///     The SignalR hub connection instance.
        /// </summary>
        private HubConnection hub;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the publisher instance asynchronously.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" /> representing the close operation.
        /// </returns>
        public async Task CloseAsync()
        {
            await Task.Run(() => this.hub.Stop());
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
        public async Task InitializeAsync(PublisherDescription description)
        {
            this.hub = new HubConnection(description.ConnectionString);
            AddClientCertificate(this.hub, description.Certificate);
            await this.hub.Start();
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
        public async Task SendAsync(IMessage message)
        {
            await this.hub.Send(message.Message);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the client certificate.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <param name="thumbprint">
        /// The thumbprint.
        /// </param>
        private static void AddClientCertificate(Connection connection, string thumbprint)
        {
            if (string.IsNullOrWhiteSpace(thumbprint))
            {
                return;
            }

            connection.AddClientCertificate(GetClientCertificate(thumbprint));
        }

        /// <summary>
        /// Gets the client certificate.
        /// </summary>
        /// <param name="thumbprint">
        /// The thumbprint.
        /// </param>
        /// <returns>
        /// The client certificate.
        /// </returns>
        private static X509Certificate2 GetClientCertificate(string thumbprint)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            return
                store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false)
                    .Cast<X509Certificate2>()
                    .FirstOrDefault();
        }

        #endregion
    }
}