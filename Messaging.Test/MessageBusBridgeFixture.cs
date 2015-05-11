// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusBridgeFixture.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // TODO Impliment your own customer SimpleMessageBridge and Override process message

    public class MySimpleMessageBridge : SimpleMessageBusBridge
    {
        public MySimpleMessageBridge()
        { 
        }

        public override System.Collections.Generic.IEnumerable<IMessage> ProcessMessage(IMessage Message)
        { // TODO: Modify This for your test case to match your code in the test console application

            Debug.Write(Message);

            var Msg = new List<IMessage>();
            Msg.Add(Message);

            return Msg;
        }

    }


    /// <summary>
    ///     The <see cref="SimpleMessageBusBridge" /> test fixture.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MessageBusBridgeFixture
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Verifies the message crosses bridge.
        /// </summary>
        /// <returns>The <see cref="Task" /></returns>
        [TestMethod]
        public async Task VerifyMessageCrossesBridge()
        {
            // Create the source message bus.
            var sourceBus = new MockMessageBus();
            var sourceEntity = Any.String();

            // Create the target message bus.
            var targetBus = new MockMessageBus();
            const string TargetEntity = "TestEventHub";

            // Setup the target callback. This is the output of the bridge.
            var called = false;
            await targetBus.RegisterHandlerAsync(TargetEntity, "TestEventHub", m => Task.Run(() => { called = true; }));
            
            // Create the message bus bridge.
            var bridgeName = Any.String();

            var bridge = new MySimpleMessageBridge();
            await
                bridge.InitializeAsync(
                    new MessageBusBridgeDescription
                        {
                            BridgeName = bridgeName, 
                            SourceBus = sourceBus, 
                            SourceEntity = sourceEntity, 
                            TargetBus = targetBus,
                            TargetEntity = TargetEntity
                        });

            // Send the message.
            var message = new MockMessage
                              {
                                  CorrelationKey = Any.String(),
                                  Message = "{\"Name\":\"TestEventHub\"}", 
                                  MessageKey = Any.String(), 
                                  PartitionKey = Any.String()
                              };
            await sourceBus.SendAsync(sourceEntity, message);

            // Assert that the message reached the other side.
            Assert.IsTrue(called, "The target callback must be called.");

            await bridge.CloseAsync();
        }

        #endregion
    }
}