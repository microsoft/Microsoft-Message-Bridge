// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockActivityListener.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   The mock activity listener.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Test
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     The mock activity listener.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MockActivityListener : ActivityListenerBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the exception args.
        /// </summary>
        public MessagingExceptionEventArgs ExceptionArgs { get; set; }

        /// <summary>
        /// Gets or sets the number of events fired.
        /// </summary>
        public int NumberEventsFired { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="monitor">
        /// The monitor.
        /// </param>
        public override void Initialize(IActivityMonitor monitor)
        {
            monitor.BridgeClosed += this.OnBridgeClosed;
            monitor.BridgeClosing += this.OnBridgeClosing;
            monitor.BridgeInitialized += this.OnBridgeInitialized;
            monitor.BridgeInitializing += this.OnBridgeInitializing;
            monitor.BridgeTransferred += this.OnBridgeTransferred;
            monitor.BridgeTransferring += this.OnBridgeTransferring;
            monitor.BridgeException += this.OnBridgeException;
            monitor.BridgeWarning += this.OnBridgeWarning;
            monitor.MessageBusWarning += this.OnMessageBusWarning;
            monitor.MessageBusException += this.OnMessageBusException;
            monitor.MessageBusClosed += this.OnMessageBusClosed;
            monitor.MessageBusClosing += this.OnMessageBusClosing;
            monitor.MessageBusRegisteredHandler += this.OnMessageBusHandlerRegistered;
            monitor.MessageBusRegisteringHandler += this.OnMessageBusHandlerRegistering;
            monitor.MessageBusSending += this.OnMessageBusMessageSending;
            monitor.MessageBusSent += this.OnMessageBusMessageSent;
            monitor.PublisherWarning += this.OnPublisherWarning;
            monitor.PublisherException += this.OnPublisherException;
            monitor.PublisherClosed += this.OnPublisherClosed;
            monitor.PublisherClosing += this.OnPublisherClosing;
            monitor.PublisherInitializing += this.OnPublisherInitializing;
            monitor.PublisherInitialized += this.OnPublisherInitialized;
            monitor.PublisherSending += this.OnPublisherSending;
            monitor.PublisherSent += this.OnPublisherSent;
            monitor.SubscriberWarning += this.OnSubscriberWarning;
            monitor.SubscriberException += this.OnSubscriberException;
            monitor.SubscriberClosed += this.OnSubscriberClosed;
            monitor.SubscriberClosing += this.OnSubscriberClosing;
            monitor.SubscriberInitialized += this.OnSubscriberInitialized;
            monitor.SubscriberInitializing += this.OnSubscriberInitializing;
            monitor.SubscriberReceived += this.OnSubscriberReceived;
            monitor.SubscriberReceiving += this.OnSubscriberReceiving;
            monitor.WorkerRunning += this.OnWorkerRunning;
            monitor.WorkerStarted += this.OnWorkerStarted;
            monitor.WorkerStarting += this.OnWorkerStarting;
            monitor.WorkerStopped += this.OnWorkerStopped;
            monitor.WorkerStopping += this.OnWorkerStopping;
            monitor.WorkerCompleted += this.OnWorkerCompleted;
            monitor.WorkerException += this.OnWorkerException;
            monitor.WorkerWarning += this.OnWorkerWarning;
            base.Initialize(monitor);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnBridgeClosed handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeClosed(object sender, BridgeClosedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeClosing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeClosing(object sender, BridgeClosingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeException handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeException(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeInitialized handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeInitialized(object sender, BridgeInitializedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeInitializing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeInitializing(object sender, BridgeInitializingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeTransferred handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeTransferred(object sender, BridgeTransferredEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeTransferring handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeTransferring(object sender, BridgeTransferringEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnBridgeWarning handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnBridgeWarning(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusClosed handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusClosed(object sender, MessageBusClosedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusClosing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusClosing(object sender, MessageBusClosingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusException handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusException(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusHandlerRegistered handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusHandlerRegistered(object sender, MessageBusRegisteredHandlerEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusHandlerRegistering handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusHandlerRegistering(object sender, MessageBusRegisteringHandlerEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusMessageSending handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusMessageSending(object sender, MessageBusSendingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusMessageSent handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusMessageSent(object sender, MessageBusSentEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnMessageBusWarning handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnMessageBusWarning(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherClosed handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherClosed(object sender, PublisherClosedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherClosing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherClosing(object sender, PublisherClosingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherException handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherException(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherInitialized handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherInitialized(object sender, PublisherInitializedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherInitializing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherInitializing(object sender, PublisherInitializingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherSending handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherSending(object sender, PublisherSendingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherSent handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherSent(object sender, PublisherSentEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnPublisherWarning handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnPublisherWarning(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberClosed handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberClosed(object sender, SubscriberClosedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberClosing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberClosing(object sender, SubscriberClosingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberException handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberException(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberInitialized handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberInitialized(object sender, SubscriberInitializedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberInitializing handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberInitializing(object sender, SubscriberInitializingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberReceived handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberReceived(object sender, SubscriberReceivedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberReceiving handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberReceiving(object sender, SubscriberReceivingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnSubscriberWarning handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnSubscriberWarning(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerCompleted handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerCompleted(object sender, WorkerCompletedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerException handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerException(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerRunning handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerRunning(object sender, WorkerRunningEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerStarted handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerStarted(object sender, WorkerStartedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerStarting handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerStarting(object sender, WorkerStartingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerStopped handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerStopped(object sender, WorkerStoppedEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerStopping handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerStopping(object sender, WorkerStoppingEventArgs args)
        {
            this.NumberEventsFired++;
        }

        /// <summary>
        /// The OnWorkerWarning handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private void OnWorkerWarning(object sender, MessagingExceptionEventArgs args)
        {
            this.ExceptionArgs = args;
            this.NumberEventsFired++;
        }

        #endregion
    }
}