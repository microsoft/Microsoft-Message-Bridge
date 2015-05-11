// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeOptions.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using CommandLine;

    /// <summary>
    ///     The bridge command line options.
    /// </summary>
    internal sealed class BridgeOptions
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>
        [Option("dcs", HelpText = "The message bus connection string.", Required = true)]
        public string DestinationConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the entity.
        /// </summary>
        /// <value>
        ///     The entity.
        /// </value>
        [Option("den", HelpText = "The message bus entity to publish on.", Required = true)]
        public string DestinationEntity { get; set; }

        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>
        ///     The factory.
        /// </value>
        [Option("dfn", HelpText = "The message bus factory type string.", Required = true)]
        public string DestinationFactory { get; set; }

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
        ///     Gets or sets the name of the output file.
        /// </summary>
        [Option('o', "out", HelpText = "The output file where incoming messages will be written.", Required = false)]
        public string OutputFileName { get; set; }

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        [Option('c', "scs", HelpText = "The connection string to the message bus.", Required = true)]
        public string SourceConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the message bus entity name.
        /// </summary>
        [Option('e', "sen", HelpText = "The entity name on the message bus being subscribed to.", Required = true)]
        public string SourceEntity { get; set; }

        /// <summary>
        ///     Gets or sets the message bus factory.
        /// </summary>
        [Option('f', "sfn", HelpText = "The type string of the message bus factory.", Required = true)]
        public string SourceFactory { get; set; }

        /// <summary>
        ///     Gets or sets the name of the subscriber or consumer group
        /// </summary>
        [Option('n', "ssn", HelpText = "The name of the subscriber.", Required = true)]
        public string SourceSubscriptionName { get; set; }

        /// <summary>
        ///     Gets or sets the storage connection string.
        /// </summary>
        [Option('s', "stcs", HelpText = "The storage connection string if required by the factory.", Required = false)]
        public string StorageConnectionString { get; set; }

        #endregion
    }
}