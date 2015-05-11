// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionHandler.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///     A set of methods to handle the subscription command line verb.
    /// </summary>
    internal class SubscriptionHandler
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the output stream writer.
        /// </summary>
        public static TextWriter OutputWriter { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Handles the specified subscription command line options.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public static void Handle(SubscriptionOptions options)
        {
            if (options.IsVerbose)
            {
                Debug.Listeners.Add(new ColoredConsoleTraceListener());
            }

            OutputWriter = !string.IsNullOrWhiteSpace(options.OutputFileName)
                               ? new StreamWriter(options.OutputFileName)
                               : Console.Out;

            var description = new MessageBusDescription
                                  {
                                      ConnectionString = options.ConnectionString, 
                                      Factory =
                                          DependencyResolver.Resolve<IMessageBusFactory>(
                                              options.Factory), 
                                      StorageConnectionString = options.StorageConnectionString
                                  };
            var bus = new MessageBus(description);
            bus.RegisterHandlerAsync(options.Entity, options.Name, OnMessageArrived).Wait();

            Debug.Print("This is a debug print.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            Console.WriteLine("Closing message bus...");
            bus.CloseAsync().ContinueWith(t => Console.WriteLine("Message bus is closed.")).Wait();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when a message arrives.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the operation.
        /// </returns>
        private static async Task OnMessageArrived(IMessage message)
        {
            await OutputWriter.WriteLineAsync(message.Message);
        }

        #endregion
    }
}