// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusBridgeDescription.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    /// <summary>
    ///     The message bus bridge description. This contains information necessary to initialize the bridge.
    /// </summary>
    public class MessageBusBridgeDescription
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the name of the bridge.
        /// </summary>
        public string BridgeName { get; set; }

        /// <summary>
        ///     Gets or sets the source message bus.
        /// </summary>
        public IMessageBus SourceBus { get; set; }

        /// <summary>
        ///     Gets or sets the source entity.
        /// </summary>
        public string SourceEntity { get; set; }

        /// <summary>
        ///     Gets or sets the target message bus.
        /// </summary>
        public IMessageBus TargetBus { get; set; }

        /// <summary>
        ///     Gets or sets the target entity.
        /// </summary>
        public string TargetEntity { get; set; }

        #endregion
    }
}