// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityMonitorFixture.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   The activity monitor fixture.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Test
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The ESP activity monitor fixture.
    /// </summary>
    [TestClass]
    public class ActivityMonitorFixture
    {
        #region Fields

        /// <summary>
        ///     The monitor.
        /// </summary>
        private ActivityMonitor monitor;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_BridgeClose_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockBridge = new Mock<IMessageBusBridge>();
            using (this.monitor.BridgeClose(mockBridge.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The BridgeClosing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The BridgeClosed event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_BridgeInitialize_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockBridge = new Mock<IMessageBusBridge>();
            var bridgeDescription = new MessageBusBridgeDescription();
            using (this.monitor.BridgeInitialize(mockBridge.Object, bridgeDescription))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The BridgeInitializing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The BridgeInitialized event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_BridgeTransfer_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockBridge = new Mock<IMessageBusBridge>();
            var mockMessage = new Mock<IMessage>();
            using (this.monitor.BridgeTransfer(mockBridge.Object, mockMessage.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The BridgeTransferring event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The BridgeTransferred event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_MessageBusClose_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockMessageBus = new Mock<IMessageBus>();
            using (this.monitor.MessageBusClose(mockMessageBus.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The MessageBusClosing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The MessageBusClosed event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_MessageBusRegisterHandler_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockMessageBus = new Mock<IMessageBus>();
            using (this.monitor.MessageBusRegisterHandler(mockMessageBus.Object, "TestEntity", "TestName"))
            {
                Assert.IsTrue(
                    listener.NumberEventsFired == 1, 
                    string.Format("The MessageBusRegisteringHandler event did not fire"));
            }

            Assert.IsTrue(
                listener.NumberEventsFired == 2, 
                string.Format("The MessageBusRegisteredHandler event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_MessageBusSend_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockMessageBus = new Mock<IMessageBus>();
            var mockMessage = new Mock<IMessage>();
            using (this.monitor.MessageBusSend(mockMessageBus.Object, mockMessage.Object, "TestEntity"))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The MessageBusSending event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The MessageBusSent event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_PublisherClose_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockPublisher = new Mock<IPublisher>();
            using (this.monitor.PublisherClose(mockPublisher.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The PublisherClosing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The PublisherClosed event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_PublisherInitialize_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockPublisher = new Mock<IPublisher>();
            using (this.monitor.PublisherInitialize(mockPublisher.Object, new PublisherDescription()))
            {
                Assert.IsTrue(
                    listener.NumberEventsFired == 1, 
                    string.Format("The PublisherInitializing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The PublisherInitialized event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_PublisherSend_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockPublisher = new Mock<IPublisher>();
            var mockMessage = new Mock<IMessage>();
            using (this.monitor.PublisherSend(mockPublisher.Object, mockMessage.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The PublisherSending event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The PublisherSent event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportBridgeExceptionWithIsWarningFalse_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockBridge = new Mock<IMessageBusBridge>();
            this.monitor.ReportBridgeException(mockBridge.Object, exception, false);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportBridgeExceptionWithIsWarningTrue_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockBridge = new Mock<IMessageBusBridge>();
            this.monitor.ReportBridgeException(mockBridge.Object, exception, true);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}",
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception,
                listener.ExceptionArgs.Exception,
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportMessageBusExceptionWithIsWarningFalse_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockMessageBus = new Mock<IMessageBus>();
            this.monitor.ReportMessageBusException(mockMessageBus.Object, exception, false);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportMessageBusExceptionWithIsWarningTrue_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockMessageBus = new Mock<IMessageBus>();
            this.monitor.ReportMessageBusException(mockMessageBus.Object, exception, true);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportPublisherExceptionWithIsWarningFalse_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockPublisher = new Mock<IPublisher>();
            this.monitor.ReportPublisherException(mockPublisher.Object, exception, false);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportPublisherExceptionWithIsWarningTrue_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockPublisher = new Mock<IPublisher>();
            this.monitor.ReportPublisherException(mockPublisher.Object, exception, true);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportSubscriberExceptionWithIsWarningFalse_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockSubscriber = new Mock<ISubscriber>();
            this.monitor.ReportSubscriberException(mockSubscriber.Object, exception, false);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportSubscriberExceptionWithIsWarningTrue_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            var mockSubscriber = new Mock<ISubscriber>();
            this.monitor.ReportSubscriberException(mockSubscriber.Object, exception, true);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportWorkerExceptionWithIsWarningFalse_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            this.monitor.ReportWorkerException("TestWorker", exception, false);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies correct exception
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_ReportWorkerExceptionWithIsWarningTrue_VerifyCorrectException()
        {
            string message = Any.String();
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var exception = new Exception(message);
            this.monitor.ReportWorkerException("TestWorker", exception, true);
            var format = string.Format(
                "The expected exception {0} does not match the actual exception {1}", 
                exception, 
                listener.ExceptionArgs.Exception);
            Assert.AreEqual(
                exception, 
                listener.ExceptionArgs.Exception, 
                format);
        }

        /// <summary>
        /// Verifies instance not null
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_StaticConstructor_VerifyInstanceIsNotNull()
        {
            Assert.IsNotNull(
                this.monitor, 
                string.Format(CultureInfo.InvariantCulture, "EspActivityMonitor.Instance returned a null reference"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_SubscriberClose_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockSubscriber = new Mock<ISubscriber>();
            using (this.monitor.SubscriberClose(mockSubscriber.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The SubscriberClosing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The SubscriberClosed event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_SubscriberInitialize_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockSubscriber = new Mock<ISubscriber>();
            using (this.monitor.SubscriberInitialize(mockSubscriber.Object, new SubscriberDescription()))
            {
                Assert.IsTrue(
                    listener.NumberEventsFired == 1, 
                    string.Format("The SubscriberInitializing event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The SubscriberInitialized event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_SubscriberReceive_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            var mockSubscriber = new Mock<ISubscriber>();
            var mockMessage = new Mock<IMessage>();
            using (this.monitor.SubscriberReceive(mockSubscriber.Object, mockMessage.Object))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The SubscriberReceive event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The SubscriberReceive event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_WorkerRun_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            using (this.monitor.WorkerRun("TestWorker"))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The WorkerRunning event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The WorkerCompleted event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_WorkerStart_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            using (this.monitor.WorkerStart("TestWorker"))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The WorkerStarting event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The WorkerStarted event did not fire"));
        }

        /// <summary>
        /// Verifies all events are fired.
        /// </summary>
        [TestMethod]
        public void EspActivityMonitor_WorkerStop_VerifyAllEventsAreFired()
        {
            var listener = this.monitor.Listeners.First() as MockActivityListener;
            listener.NumberEventsFired = 0;
            using (this.monitor.WorkerStop("TestWorker"))
            {
                Assert.IsTrue(listener.NumberEventsFired == 1, string.Format("The WorkerStopping event did not fire"));
            }

            Assert.IsTrue(listener.NumberEventsFired == 2, string.Format("The WorkerStopped event did not fire"));
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.monitor = ActivityMonitor.Instance;
        }

        #endregion
    }
}