// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using CommandLine;
    using CommandLine.Text;

    /// <summary>
    ///     The base command line options for this program.
    /// </summary>
    internal class CommandLineOptions
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the bridge verb options.
        /// </summary>
        /// <value>
        ///     The bridge.
        /// </value>
        [VerbOption("bridge", HelpText = "Runs as a bridge client.")]
        public BridgeOptions Bridge { get; set; }

        /// <summary>
        ///     Gets or sets the publish verb options.
        /// </summary>
        [VerbOption("pub", HelpText = "Runs as a publisher client.")]
        public PublicationOptions Publish { get; set; }

        /// <summary>
        ///     Gets or sets the subscribe verb options.
        /// </summary>
        [VerbOption("sub", HelpText = "Runs as a subscription client.")]
        public SubscriptionOptions Subscribe { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <param name="verb">
        /// The verb.
        /// </param>
        /// <returns>
        /// The help text.
        /// </returns>
        [HelpVerbOption("help")]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }

        #endregion
    }
}