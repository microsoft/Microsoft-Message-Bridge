// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeHandler.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using System;
    using System.Diagnostics;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     A set of methods to handle the bridge command line verb.
    /// </summary>
    /// 

    public class MySimpleMessageBridge : SimpleMessageBusBridge
    {
        public MySimpleMessageBridge()
        {
        }

        public override System.Collections.Generic.IEnumerable<IMessage> ProcessMessage(IMessage Message)
        {
            // TODO: Modify this function to modify the Message in Process Message to sute your need. 
            
            Debug.Write(Message);
            var Msgs = new List<IMessage>();
            Msgs.Add(Message);
            return Msgs;
        }

    }

    internal class BridgeHandler
    {
        #region Public Methods and Operators

        /// <summary>
        /// Handles the specified bridge command line options.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public static void Handle(BridgeOptions options)
        {
            if (options.IsVerbose)
            {
                Debug.Listeners.Add(new ColoredConsoleTraceListener());
            }

            var destination = CreateMessageBus(
                options.DestinationConnectionString, 
                options.DestinationFactory, 
                string.Empty);
            var source = CreateMessageBus(
                options.SourceConnectionString, 
                options.SourceFactory, 
                options.StorageConnectionString);
            var description = new MessageBusBridgeDescription
                                  {
                                      BridgeName = "GoldenBridge",
                                      SourceBus = source,
                                      SourceEntity = options.SourceEntity,
                                      TargetBus = destination,
                                      TargetEntity = options.DestinationEntity
                                  };

            var bridge = new MySimpleMessageBridge();
            bridge.InitializeAsync(description).Wait();

            Console.WriteLine("Press any key to exist...");
            Console.ReadKey();

            Console.WriteLine("Closing bridge...");
            bridge.CloseAsync().ContinueWith(t => Console.WriteLine("Bridge is closed.")).Wait();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the message bus.
        /// </summary>
        /// <param name="cs">
        /// The connection string.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="storage">
        /// The storage connection string.
        /// </param>
        /// <returns>
        /// The <see cref="IMessageBus"/> instance.
        /// </returns>
        private static IMessageBus CreateMessageBus(string cs, string factory, string storage)
        {
            var description = new MessageBusDescription
                                  {
                                      ConnectionString = cs, 
                                      Factory =
                                          DependencyResolver.Resolve<IMessageBusFactory>(factory), 
                                      StorageConnectionString = storage
                                  };
            return new MessageBus(description);
        }

        #endregion
    }
}