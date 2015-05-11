// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugActivityListener.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.Debug
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     Monitors the Activity Monitor and produces Debug statements for each .
    /// </summary>
    public class DebugActivityListener : ActivityListenerBase
    {
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
        private void OnBridgeClosed(object sender, BridgeClosedEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Closed ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnBridgeClosing(object sender, BridgeClosingEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Closing.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnBridgeException(object sender, MessagingExceptionEventArgs args)
        {
            Debug.Print(
                "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] Exception: {4}", 
                this.MachineName, 
                this.DeploymentId, 
                this.RoleInstanceName, 
                this.RoleInstanceId, 
                args.Exception);
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
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Initialized ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnBridgeInitializing(object sender, BridgeInitializingEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Initializing.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnBridgeTransferred(object sender, BridgeTransferredEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Transferred message {5} ({6:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnBridgeTransferring(object sender, BridgeTransferringEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Transferring message {5}.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnBridgeWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IMessageBusBridge)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Bridge: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusClosed(object sender, MessageBusClosedEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Closed ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusClosing(object sender, MessageBusClosingEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Closing.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IMessageBus)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Bus: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusHandlerRegistered(object sender, MessageBusRegisteredHandlerEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Registered handler {5} for {6} ({7:F6}).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Name, 
                    args.Entity, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusHandlerRegistering(object sender, MessageBusRegisteringHandlerEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Registering handler {5} for {6}.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Name, 
                    args.Entity);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusMessageSending(object sender, MessageBusSendingEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Sending message {5} to {6}.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Entity);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusMessageSent(object sender, MessageBusSentEventArgs args)
        {
            try
            {
                var bus = (IMessageBus)sender;
                var name = bus.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Sent message {5} to {6} ({7:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Entity, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnMessageBusWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IMessageBus)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Bus: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherClosed(object sender, PublisherClosedEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Closed ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherClosing(object sender, PublisherClosingEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Closing.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IPublisher)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Publisher: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherInitialized(object sender, PublisherInitializedEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Initialized {5} ({6:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Entity, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherInitializing(object sender, PublisherInitializingEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Initializing {5} ({6}).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Entity, 
                    args.Description.ConnectionString);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherSending(object sender, PublisherSendingEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Sending message {5}.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherSent(object sender, PublisherSentEventArgs args)
        {
            try
            {
                var publisher = (IPublisher)sender;
                var name = publisher.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Sent message {5} ({6:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnPublisherWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (IPublisher)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Publisher: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberClosed(object sender, SubscriberClosedEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Closed ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberClosing(object sender, SubscriberClosingEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Closing.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (ISubscriber)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Subscriber: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberInitialized(object sender, SubscriberInitializedEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Initialized {5} on {6} ({7:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Name, 
                    args.Description.Entity, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberInitializing(object sender, SubscriberInitializingEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Initializing {5} on {6} ({7}).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Description.Name, 
                    args.Description.Entity, 
                    args.Description.ConnectionString);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberReceived(object sender, SubscriberReceivedEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Received message {5} ({6:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey, 
                    args.Elapsed.TotalSeconds);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberReceiving(object sender, SubscriberReceivingEventArgs args)
        {
            try
            {
                var subscriber = (ISubscriber)sender;
                var name = subscriber.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Receiving message {5}.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Message.MessageKey);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnSubscriberWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                var bridge = (ISubscriber)sender;
                var name = bridge.GetType().Name;

                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] [Subscriber: {4} Exception: {5}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    name, 
                    args.Exception);
            }
            catch (InvalidCastException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (NullReferenceException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerCompleted(object sender, WorkerCompletedEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Completed ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender, 
                    args.Elapsed.TotalSeconds);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerException(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] Exception: {4}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    args.Exception);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerRunning(object sender, WorkerRunningEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Running.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerStarted(object sender, WorkerStartedEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Started ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender, 
                    args.Elapsed.TotalSeconds);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerStarting(object sender, WorkerStartingEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Starting.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerStopped(object sender, WorkerStoppedEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Stopped ({5:F6} seconds).", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender, 
                    args.Elapsed.TotalSeconds);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerStopping(object sender, WorkerStoppingEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Stopping.", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    sender);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
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
        private void OnWorkerWarning(object sender, MessagingExceptionEventArgs args)
        {
            try
            {
                Debug.Print(
                    "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] Exception: {4}", 
                    this.MachineName, 
                    this.DeploymentId, 
                    this.RoleInstanceName, 
                    this.RoleInstanceId, 
                    args.Exception);
            }
            catch (ArgumentNullException ex)
            {
                Debug.Print(ex.ToString());
            }
            catch (FormatException ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        #endregion
    }
}