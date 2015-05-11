// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberDescription.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    /// <summary>
    ///     Describes how an <see cref="ISubscriber" /> instance should connect to its message bus.
    /// </summary>
    public class SubscriberDescription : DescriptionBase
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the name of the subscriber.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the storage connection string.
        /// </summary>
        public string StorageConnectionString { get; set; }

        #endregion
    }
}