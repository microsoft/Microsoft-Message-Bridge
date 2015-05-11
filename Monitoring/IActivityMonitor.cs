// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IActivityMonitor.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   The ActivityMonitor interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The ActivityMonitor interface.
    /// </summary>
    public interface IActivityMonitor
    {
        #region Public Events

        /// <summary>
        ///     The bridge closed event.
        /// </summary>
        event EventHandler<BridgeClosedEventArgs> BridgeClosed;

        /// <summary>
        ///     The bridge closing event.
        /// </summary>
        event EventHandler<BridgeClosingEventArgs> BridgeClosing;

        /// <summary>
        ///     The bridge exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> BridgeException;

        /// <summary>
        ///     The bridge initialized event.
        /// </summary>
        event EventHandler<BridgeInitializedEventArgs> BridgeInitialized;

        /// <summary>
        ///     The bridge initializing event.
        /// </summary>
        event EventHandler<BridgeInitializingEventArgs> BridgeInitializing;

        /// <summary>
        ///     The bridge transferred event.
        /// </summary>
        event EventHandler<BridgeTransferredEventArgs> BridgeTransferred;

        /// <summary>
        ///     The bridge transferring event.
        /// </summary>
        event EventHandler<BridgeTransferringEventArgs> BridgeTransferring;

        /// <summary>
        ///     The bridge warning event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> BridgeWarning;

        /// <summary>
        ///     The message bus closed.
        /// </summary>
        event EventHandler<MessageBusClosedEventArgs> MessageBusClosed;

        /// <summary>
        ///     The messaging bus closing event.
        /// </summary>
        event EventHandler<MessageBusClosingEventArgs> MessageBusClosing;

        /// <summary>
        ///     The message bus exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> MessageBusException;

        /// <summary>
        ///     The message bus registered handler event.
        /// </summary>
        event EventHandler<MessageBusRegisteredHandlerEventArgs> MessageBusRegisteredHandler;

        /// <summary>
        ///     The message bus registering handler event.
        /// </summary>
        event EventHandler<MessageBusRegisteringHandlerEventArgs> MessageBusRegisteringHandler;

        /// <summary>
        ///     The message bus sending event.
        /// </summary>
        event EventHandler<MessageBusSendingEventArgs> MessageBusSending;

        /// <summary>
        ///     The message bus sent event.
        /// </summary>
        event EventHandler<MessageBusSentEventArgs> MessageBusSent;

        /// <summary>
        ///     The message bus exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> MessageBusWarning;

        /// <summary>
        ///     The publisher closed event.
        /// </summary>
        event EventHandler<PublisherClosedEventArgs> PublisherClosed;

        /// <summary>
        ///     The publisher closing event.
        /// </summary>
        event EventHandler<PublisherClosingEventArgs> PublisherClosing;

        /// <summary>
        ///     The publisher exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> PublisherException;

        /// <summary>
        ///     The publisher initialized event.
        /// </summary>
        event EventHandler<PublisherInitializedEventArgs> PublisherInitialized;

        /// <summary>
        ///     The publisher initializing event.
        /// </summary>
        event EventHandler<PublisherInitializingEventArgs> PublisherInitializing;

        /// <summary>
        ///     The publisher sending event.
        /// </summary>
        event EventHandler<PublisherSendingEventArgs> PublisherSending;

        /// <summary>
        ///     The publisher sent event.
        /// </summary>
        event EventHandler<PublisherSentEventArgs> PublisherSent;

        /// <summary>
        ///     The publisher exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> PublisherWarning;

        /// <summary>
        ///     The subscriber closed event.
        /// </summary>
        event EventHandler<SubscriberClosedEventArgs> SubscriberClosed;

        /// <summary>
        ///     The subscriber closing event.
        /// </summary>
        event EventHandler<SubscriberClosingEventArgs> SubscriberClosing;

        /// <summary>
        ///     The subscriber exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> SubscriberException;

        /// <summary>
        ///     The subscriber initialized event.
        /// </summary>
        event EventHandler<SubscriberInitializedEventArgs> SubscriberInitialized;

        /// <summary>
        ///     The subscriber initializing event.
        /// </summary>
        event EventHandler<SubscriberInitializingEventArgs> SubscriberInitializing;

        /// <summary>
        ///     The subscriber received event.
        /// </summary>
        event EventHandler<SubscriberReceivedEventArgs> SubscriberReceived;

        /// <summary>
        ///     The subscriber receiving event.
        /// </summary>
        event EventHandler<SubscriberReceivingEventArgs> SubscriberReceiving;

        /// <summary>
        ///     The subscriber exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> SubscriberWarning;

        /// <summary>
        ///     The worker completed event.
        /// </summary>
        event EventHandler<WorkerCompletedEventArgs> WorkerCompleted;

        /// <summary>
        ///     The worker exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> WorkerException;

        /// <summary>
        ///     The worker running event.
        /// </summary>
        event EventHandler<WorkerRunningEventArgs> WorkerRunning;

        /// <summary>
        ///     The worker started event.
        /// </summary>
        event EventHandler<WorkerStartedEventArgs> WorkerStarted;

        /// <summary>
        ///     The worker starting event.
        /// </summary>
        event EventHandler<WorkerStartingEventArgs> WorkerStarting;

        /// <summary>
        ///     The worker stopped event.
        /// </summary>
        event EventHandler<WorkerStoppedEventArgs> WorkerStopped;

        /// <summary>
        ///     The worker stopping event.
        /// </summary>
        event EventHandler<WorkerStoppingEventArgs> WorkerStopping;

        /// <summary>
        ///     The worker exception event.
        /// </summary>
        event EventHandler<MessagingExceptionEventArgs> WorkerWarning;

        /// <summary>
        /// Occurs when [subscriber message count].
        /// </summary>
        event EventHandler<SubscriberMessageCountEventArgs> SubscriberMessageCount;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create listeners.
        /// </summary>
        /// <returns>
        ///     IEnumerable of IActivityListener
        /// </returns>
        IEnumerable<IActivityListener> CreateListeners();

        #endregion
    }
}