// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTraceActivityListener.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.EventTracing
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The event trace activity listener.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", 
        Justification =
            "This class will be instantiated once and remain for the lifetime of the application. There is no need to dispose this type.")]
    public class EventTraceActivityListener : ActivityListenerBase
    {
        #region Fields

        /// <summary>
        ///     The bridge event writer.
        /// </summary>
        private BridgeEventTraceWriter bridgeEventWriter;

        /// <summary>
        ///     The message bus event writer.
        /// </summary>
        private MessageBusEventTraceWriter messageBusEventWriter;

        /// <summary>
        ///     The publisher event writer.
        /// </summary>
        private PublisherEventTraceWriter publisherEventWriter;

        /// <summary>
        ///     The subscriber event writer.
        /// </summary>
        private SubscriberEventTraceWriter subscriberEventWriter;

        /// <summary>
        ///     The worker event writer.
        /// </summary>
        private WorkerEventTraceWriter workerEventWriter;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The initialize method for this listener instance.
        /// </summary>
        /// <param name="monitor">
        /// The monitor.
        /// </param>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", 
            Justification = "This is by design.")]
        public override void Initialize(IActivityMonitor monitor)
        {
            base.Initialize(monitor);

            if (monitor == null)
            {
                return;
            }

            this.bridgeEventWriter = new BridgeEventTraceWriter();
            this.messageBusEventWriter = new MessageBusEventTraceWriter();
            this.publisherEventWriter = new PublisherEventTraceWriter();
            this.subscriberEventWriter = new SubscriberEventTraceWriter();
            this.workerEventWriter = new WorkerEventTraceWriter();

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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeClosed(object sender, BridgeClosedEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;
                this.bridgeEventWriter.Closed(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeClosing(object sender, BridgeClosingEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Closing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Error(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeInitialized(object sender, BridgeInitializedEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Initialized(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeInitializing(object sender, BridgeInitializingEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Initializing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeTransferred(object sender, BridgeTransferredEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Transferred(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeTransferring(object sender, BridgeTransferringEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Transferring(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnBridgeWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                this.bridgeEventWriter.Warning(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusClosed(object sender, MessageBusClosedEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.Closed(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusClosing(object sender, MessageBusClosingEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.Closing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.Error(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusHandlerRegistered(object sender, MessageBusRegisteredHandlerEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.Registered(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Name, 
                    args.Entity, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusHandlerRegistering(object sender, MessageBusRegisteringHandlerEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.Registering(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Name, 
                    args.Entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusMessageSending(object sender, MessageBusSendingEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.MessageSending(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusMessageSent(object sender, MessageBusSentEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.MessageSent(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Entity, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnMessageBusWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                this.messageBusEventWriter.Warning(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherClosed(object sender, PublisherClosedEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Closed(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherClosing(object sender, PublisherClosingEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Closing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Error(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherInitialized(object sender, PublisherInitializedEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Initialized(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Entity, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherInitializing(object sender, PublisherInitializingEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Initializing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Entity, 
                    args.Description.ConnectionString);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherSending(object sender, PublisherSendingEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Sending(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherSent(object sender, PublisherSentEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Sent(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnPublisherWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                this.publisherEventWriter.Warning(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberClosed(object sender, SubscriberClosedEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Closed(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberClosing(object sender, SubscriberClosingEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Closing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Error(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberInitialized(object sender, SubscriberInitializedEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Initialized(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Name, 
                    args.Description.Entity, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.CurrentCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberInitializing(object sender, SubscriberInitializingEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Initializing(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Name, 
                    args.Description.Entity, 
                    args.Description.ConnectionString);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberReceived(object sender, SubscriberReceivedEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Received(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberReceiving(object sender, SubscriberReceivingEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Receiving(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnSubscriberWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                this.subscriberEventWriter.Warning(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerCompleted(object sender, WorkerCompletedEventArgs args)
        {
            try
            {
                this.workerEventWriter.Completed(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString(), 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                this.workerEventWriter.Error(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString(), 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerRunning(object sender, WorkerRunningEventArgs args)
        {
            try
            {
                this.workerEventWriter.Running(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerStarted(object sender, WorkerStartedEventArgs args)
        {
            try
            {
                this.workerEventWriter.Started(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString(), 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerStarting(object sender, WorkerStartingEventArgs args)
        {
            try
            {
                this.workerEventWriter.Starting(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerStopped(object sender, WorkerStoppedEventArgs args)
        {
            try
            {
                this.workerEventWriter.Stopped(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString(), 
                    args.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerStopping(object sender, WorkerStoppingEventArgs args)
        {
            try
            {
                this.workerEventWriter.Stopping(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception type is not known.")]
        private void OnWorkerWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                this.workerEventWriter.Warning(
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender.ToString(), 
                    args.Exception.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        #endregion
    }
}