// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using System;

    using CommandLine;
    using CommandLine.Text;

    /// <summary>
    ///     The application program class.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">
        /// The program command line arguments.
        /// </param>
        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            var verb = string.Empty;
            object instance = null;

            if (!Parser.Default.ParseArguments(
                args, 
                options, 
                (v, i) =>
                    {
                        verb = v;
                        instance = i;
                    }))
            {
                return;
            }

            // Display program banner
            Console.WriteLine(HelpText.AutoBuild(new { }));
            Console.WriteLine();

            switch (verb)
            {
                case "sub":
                    SubscriptionHandler.Handle((SubscriptionOptions)instance);
                    break;

                case "pub":
                    PublicationHandler.Handle((PublicationOptions)instance);
                    break;
                case "bridge":
                    BridgeHandler.Handle((BridgeOptions)instance);
                    break;
            }
        }

        #endregion
    }
}