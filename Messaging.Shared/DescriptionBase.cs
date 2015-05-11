// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DescriptionBase.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    /// <summary>
    ///     The description base type.
    /// </summary>
    public abstract class DescriptionBase
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the bus entity.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets the certificate thumbprint.
        /// </summary>
        public string Certificate { get; set; }

        #endregion
    }
}