// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublicationOptions.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using CommandLine;

    /// <summary>
    ///     The publication command line options.
    /// </summary>
    internal sealed class PublicationOptions
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>
        [Option('c', HelpText = "The message bus connection string.", Required = true)]
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the entity.
        /// </summary>
        /// <value>
        ///     The entity.
        /// </value>
        [Option('e', HelpText = "The message bus entity to publish on.", Required = true)]
        public string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>
        ///     The factory.
        /// </value>
        [Option('f', HelpText = "The message bus factory type string.", Required = true)]
        public string Factory { get; set; }

        /// <summary>
        ///     Gets or sets the name of the input file.
        /// </summary>
        /// <value>
        ///     The name of the input file.
        /// </value>
        [Option('i', HelpText = "The path to a file that contains a collection of messages to send.", Required = false)]
        public string InputFileName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether verbose output will be displayed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if verbose output is displayed; otherwise, <c>false</c>.
        /// </value>
        [Option('v', "verbse", HelpText = "Indicates whether verbose output will be displayed.", Required = false, 
            DefaultValue = false)]
        public bool IsVerbose { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        [Option('m', HelpText = "The message text to publish.", Required = false)]
        public string Message { get; set; }

        #endregion
    }
}