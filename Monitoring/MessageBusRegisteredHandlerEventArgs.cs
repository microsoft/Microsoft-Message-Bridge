// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusRegisteredHandlerEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    /// <summary>
    ///     The data for the event where the message bus has registered a handler.
    /// </summary>
    public class MessageBusRegisteredHandlerEventArgs : EndEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the entity.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}