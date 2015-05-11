// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebPublisher.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Web
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    /// <summary>
    ///     Implements the publisher interface for HTTP web calls.
    /// </summary>
    public class WebPublisher : IPublisher
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the certificate.
        /// </summary>
        /// <value>
        ///     The certificate.
        /// </value>
        private X509Certificate2 Certificate { get; set; }

        /// <summary>
        ///     Gets or sets the publish URI.
        /// </summary>
        /// <value>
        ///     The publish URI.
        /// </value>
        private Uri PublishUri { get; set; }

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
            await Task.Run(() => { });
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
            await Task.Run(
                () =>
                    {
                        var baseUri = new Uri(NormalizeConnectionString(description.ConnectionString));
                        var entityUri = new Uri(description.Entity, UriKind.Relative);
                        this.PublishUri = new Uri(baseUri, entityUri);
                        this.Certificate = GetClientCertificate(description.Certificate);
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
        public async Task SendAsync(IMessage message)
        {
            using (var client = new SecureWebClient(this.Certificate))
            {
                await client.UploadStringTaskAsync(this.PublishUri, message.Message);
            }
        }

        #endregion

        #region Methods

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
            if (string.IsNullOrWhiteSpace(thumbprint))
            {
                return null;
            }

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            return
                store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false)
                    .Cast<X509Certificate2>()
                    .FirstOrDefault();
        }

        /// <summary>
        /// Normalizes the connection string. The connection string must end with a forward slash. This method looks to
        ///     see that it does. If it does not then it is appended.
        /// </summary>
        /// <param name="cs">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The normalized connection string.
        /// </returns>
        private static string NormalizeConnectionString(string cs)
        {
            if (cs.EndsWith("/", StringComparison.Ordinal))
            {
                return cs;
            }

            return cs + "/";
        }

        #endregion

        /// <summary>
        /// An internal secure <see cref="WebClient"/>.
        /// </summary>
        private class SecureWebClient : WebClient
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SecureWebClient"/> class.
            /// </summary>
            /// <param name="certificate">The certificate.</param>
            public SecureWebClient(X509Certificate2 certificate)
            {
                this.Certificate = certificate;
            }

            /// <summary>
            /// Gets or sets the certificate.
            /// </summary>
            /// <value>
            /// The certificate.
            /// </value>
            private X509Certificate2 Certificate { get; set; }

            /// <summary>
            /// Returns a <see cref="T:System.Net.WebRequest"/> object for the specified resource.
            /// </summary>
            /// <param name="address">
            /// A <see cref="T:System.Uri"/> that identifies the resource to request.
            /// </param>
            /// <returns>
            /// A new <see cref="T:System.Net.WebRequest"/> object for the specified resource.
            /// </returns>
            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address) as HttpWebRequest;

                if (request != null && this.Certificate != null)
                {
                    request.ClientCertificates = new X509CertificateCollection { this.Certificate };
                }

                return request;
            }
        }
    }
}