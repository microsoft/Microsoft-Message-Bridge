// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockStartingEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Test
{
    /// <summary>
    ///     The mock starting event arguments.
    /// </summary>
    internal class MockStartingEventArgs : BeginEventArgs
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        /// <value>
        ///     The context.
        /// </value>
        public string Context { get; set; }

        #endregion
    }
}