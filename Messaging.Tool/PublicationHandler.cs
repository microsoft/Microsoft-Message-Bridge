// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublicationHandler.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    ///     A set of methods that handle the publish command line verb.
    /// </summary>
    internal class PublicationHandler
    {
        #region Public Methods and Operators

        /// <summary>
        /// Handles the specified publish verb options.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public static void Handle(PublicationOptions options)
        {
            if (options.IsVerbose)
            {
                Debug.Listeners.Add(new ConsoleTraceListener());
            }

            var description = new MessageBusDescription
                                  {
                                      ConnectionString = options.ConnectionString, 
                                      Factory =
                                          DependencyResolver.Resolve<IMessageBusFactory>(
                                              options.Factory)
                                  };

            var bus = new MessageBus(description);

            var entity = options.Entity;

            var message = options.Message;

            if (!string.IsNullOrWhiteSpace(message))
            {
                SendMessage(entity, message, bus);
            }

            var fileName = options.InputFileName;

            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
            {
                foreach (var line in File.ReadAllLines(fileName))
                {
                    SendMessage(entity, line, bus);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="bus">
        /// The bus.
        /// </param>
        private static void SendMessage(string entity, string message, MessageBus bus)
        {
            Console.WriteLine("Sending message: {0}", message);
            bus.SendAsync(entity, new EventMessage { Message = message, MessageKey = Guid.NewGuid().ToString() }).Wait();
        }

        #endregion
    }
}