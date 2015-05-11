// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusFixture.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="MessageBus" /> test fixture.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MessageBusFixture
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Verifies the published messages are received.
        /// </summary>
        /// <returns>A <see cref="Task" /></returns>
        [TestMethod]
        public async Task VerifyMessagesArePublished()
        {
            // Setup the message bus.
            var cs = Any.String();
            var publisher = new MockPublisher();
            var mbf = new MockMessageBusFactory { Publisher = publisher };
            var mbd = new MessageBusDescription { ConnectionString = cs, Factory = mbf };
            var bus = new MessageBus(mbd);
            var entity = Any.String();

            Assert.IsFalse(
                publisher.IsInitialized, 
                "The publisher should not be initialized before it is called the first time.");

            // Send a message on the bus.
            var message = new MockMessage
                              {
                                  CorrelationKey = Any.String(), 
                                  Message = Any.String(), 
                                  MessageKey = Any.String(), 
                                  PartitionKey = Any.String(), 
                                  Properties =
                                      new Dictionary<string, object> { { Any.String(), Any.String() } }
                              };
            await bus.SendAsync(entity, message);

            Assert.IsTrue(
                publisher.IsInitialized, 
                "The publisher should be initialized after it is called the first time.");
            Assert.AreEqual(publisher.Description.ConnectionString, cs);
            Assert.AreEqual(publisher.Description.Entity, entity);

            await bus.CloseAsync();

            Assert.IsTrue(publisher.IsClosed, "The publisher should be closed.");

            // Verify the message sent.
            Assert.AreEqual(publisher.IsInitialized, true);
            Assert.AreEqual(publisher.Message.CorrelationKey, message.CorrelationKey);
            Assert.AreEqual(publisher.Message.Message, message.Message);
            Assert.AreEqual(publisher.Message.MessageKey, message.MessageKey);
            Assert.AreEqual(publisher.Message.PartitionKey, message.PartitionKey);
        }

        /// <summary>
        ///     Verifies the subscriber is initialized when registered.
        /// </summary>
        /// <returns>A <see cref="Task" /></returns>
        [TestMethod]
        public async Task VerifySubscriberIsInitializedWhenRegistered()
        {
            // Setup the message bus.
            var cs = Any.String();
            var subscriber = new MockSubscriber();
            var mbf = new MockMessageBusFactory { Subscriber = subscriber };
            var mbd = new MessageBusDescription { ConnectionString = cs, Factory = mbf };
            var bus = new MessageBus(mbd);
            var entity = "MessageSendSuccess;MessageSendFailure";
            var name = "MessageSendSuccess";

            Assert.IsFalse(
                subscriber.IsInitialized, 
                "The subscriber should not be initialized before it is registered.");

            var called = false;

            // Register the subscriber.
            await bus.RegisterHandlerAsync(entity, name, message => Task.Run(() => { called = true; }));

            Assert.IsTrue(subscriber.IsInitialized, "The subscriber should be initialized after it is registered.");
            Assert.AreEqual(subscriber.Description.ConnectionString, cs);
            Assert.AreEqual(subscriber.Description.Entity, entity);
            Assert.AreEqual(subscriber.Description.Name, name);

            await subscriber.Handler.Invoke(null);
            Assert.IsTrue(called, "The handler must be callable.");

            // Close the bus.
            await bus.CloseAsync();

            Assert.IsTrue(subscriber.IsClosed, "The subscriber should be closed after the bus is closed.");
        }

        #endregion
    }
}