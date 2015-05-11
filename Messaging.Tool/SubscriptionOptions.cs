// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionOptions.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using CommandLine;

    /// <summary>
    ///     Describes the subscription verb command line options.
    /// </summary>
    internal sealed class SubscriptionOptions
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        [Option('c', HelpText = "The connection string to the message bus.", Required = true)]
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the message bus entity name.
        /// </summary>
        [Option('e', HelpText = "The entity name on the message bus being subscribed to.", Required = true)]
        public string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the message bus factory.
        /// </summary>
        [Option('f', HelpText = "The type string of the message bus factory.", Required = true)]
        public string Factory { get; set; }

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
        ///     Gets or sets the name of the subscriber.
        /// </summary>
        [Option('n', HelpText = "The name of the subscriber.", Required = true)]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name of the output file.
        /// </summary>
        [Option('o', HelpText = "The output file where incoming messages will be written.", Required = false)]
        public string OutputFileName { get; set; }

        /// <summary>
        ///     Gets or sets the storage connection string.
        /// </summary>
        [Option('s', HelpText = "The storage connection string if required by the factory.", Required = false)]
        public string StorageConnectionString { get; set; }

        #endregion
    }
}