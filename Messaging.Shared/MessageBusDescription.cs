// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusDescription.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    /// <summary>
    ///     Describes how to connect to the message bus instance.
    /// </summary>
    public class MessageBusDescription
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the certificate.
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the message bus factory.
        /// </summary>
        public IMessageBusFactory Factory { get; set; }

        /// <summary>
        ///     Gets or sets the storage connection string.
        /// </summary>
        public string StorageConnectionString { get; set; }

        #endregion
    }
}