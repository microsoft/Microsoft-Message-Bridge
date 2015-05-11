// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockEndingEventArgs.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Test
{
    /// <summary>
    ///     The mock ending event arguments.
    /// </summary>
    internal class MockEndingEventArgs : EndEventArgs
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