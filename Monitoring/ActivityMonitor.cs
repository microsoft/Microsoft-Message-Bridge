// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityMonitor.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Timers;

    using Microsoft.MessageBridge.Messaging;

    /// <summary>
    ///     The activity monitor which produces events that listeners subscribe to.
    /// </summary>
    public sealed class ActivityMonitor : IActivityMonitor
    {
        #region Static Fields

        /// <summary>
        ///     The single instance of the activity monitor.
        /// </summary>
        private static readonly ActivityMonitor Monitor = new ActivityMonitor();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="ActivityMonitor" /> class from being created.
        /// </summary>
        private ActivityMonitor()
        {
            this.Initialize();
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     The bridge closed event.
        /// </summary>
        public event EventHandler<BridgeClosedEventArgs> BridgeClosed;

        /// <summary>
        ///     The bridge closing event.
        /// </summary>
        public event EventHandler<BridgeClosingEventArgs> BridgeClosing;

        /// <summary>
        ///     The bridge exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> BridgeException;

        /// <summary>
        ///     The bridge initialized event.
        /// </summary>
        public event EventHandler<BridgeInitializedEventArgs> BridgeInitialized;

        /// <summary>
        ///     The bridge initializing event.
        /// </summary>
        public event EventHandler<BridgeInitializingEventArgs> BridgeInitializing;

        /// <summary>
        ///     The bridge transferred event.
        /// </summary>
        public event EventHandler<BridgeTransferredEventArgs> BridgeTransferred;

        /// <summary>
        ///     The bridge transferring event.
        /// </summary>
        public event EventHandler<BridgeTransferringEventArgs> BridgeTransferring;

        /// <summary>
        ///     The bridge warning event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> BridgeWarning;

        /// <summary>
        ///     The message bus closed.
        /// </summary>
        public event EventHandler<MessageBusClosedEventArgs> MessageBusClosed;

        /// <summary>
        ///     The messaging bus closing event.
        /// </summary>
        public event EventHandler<MessageBusClosingEventArgs> MessageBusClosing;

        /// <summary>
        ///     The message bus exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> MessageBusException;

        /// <summary>
        ///     The message bus registered handler event.
        /// </summary>
        public event EventHandler<MessageBusRegisteredHandlerEventArgs> MessageBusRegisteredHandler;

        /// <summary>
        ///     The message bus registering handler event.
        /// </summary>
        public event EventHandler<MessageBusRegisteringHandlerEventArgs> MessageBusRegisteringHandler;

        /// <summary>
        ///     The message bus sending event.
        /// </summary>
        public event EventHandler<MessageBusSendingEventArgs> MessageBusSending;

        /// <summary>
        ///     The message bus sent event.
        /// </summary>
        public event EventHandler<MessageBusSentEventArgs> MessageBusSent;

        /// <summary>
        ///     The message bus exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> MessageBusWarning;

        /// <summary>
        ///     The publisher closed event.
        /// </summary>
        public event EventHandler<PublisherClosedEventArgs> PublisherClosed;

        /// <summary>
        ///     The publisher closing event.
        /// </summary>
        public event EventHandler<PublisherClosingEventArgs> PublisherClosing;

        /// <summary>
        ///     The publisher exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> PublisherException;

        /// <summary>
        ///     The publisher initialized event.
        /// </summary>
        public event EventHandler<PublisherInitializedEventArgs> PublisherInitialized;

        /// <summary>
        ///     The publisher initializing event.
        /// </summary>
        public event EventHandler<PublisherInitializingEventArgs> PublisherInitializing;

        /// <summary>
        ///     The publisher sending event.
        /// </summary>
        public event EventHandler<PublisherSendingEventArgs> PublisherSending;

        /// <summary>
        ///     The publisher sent event.
        /// </summary>
        public event EventHandler<PublisherSentEventArgs> PublisherSent;

        /// <summary>
        ///     The publisher exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> PublisherWarning;

        /// <summary>
        ///     The subscriber closed event.
        /// </summary>
        public event EventHandler<SubscriberClosedEventArgs> SubscriberClosed;

        /// <summary>
        ///     The subscriber closing event.
        /// </summary>
        public event EventHandler<SubscriberClosingEventArgs> SubscriberClosing;

        /// <summary>
        ///     The subscriber exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> SubscriberException;

        /// <summary>
        ///     The subscriber initialized event.
        /// </summary>
        public event EventHandler<SubscriberInitializedEventArgs> SubscriberInitialized;

        /// <summary>
        ///     The subscriber initializing event.
        /// </summary>
        public event EventHandler<SubscriberInitializingEventArgs> SubscriberInitializing;

        /// <summary>
        ///     The subscriber received event.
        /// </summary>
        public event EventHandler<SubscriberReceivedEventArgs> SubscriberReceived;

        /// <summary>
        ///     The subscriber receiving event.
        /// </summary>
        public event EventHandler<SubscriberReceivingEventArgs> SubscriberReceiving;

        /// <summary>
        ///     The subscriber exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> SubscriberWarning;

        /// <summary>
        ///     The worker completed event.
        /// </summary>
        public event EventHandler<WorkerCompletedEventArgs> WorkerCompleted;

        /// <summary>
        ///     The worker exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> WorkerException;

        /// <summary>
        ///     The worker running event.
        /// </summary>
        public event EventHandler<WorkerRunningEventArgs> WorkerRunning;

        /// <summary>
        ///     The worker started event.
        /// </summary>
        public event EventHandler<WorkerStartedEventArgs> WorkerStarted;

        /// <summary>
        ///     The worker starting event.
        /// </summary>
        public event EventHandler<WorkerStartingEventArgs> WorkerStarting;

        /// <summary>
        ///     The worker stopped event.
        /// </summary>
        public event EventHandler<WorkerStoppedEventArgs> WorkerStopped;

        /// <summary>
        ///     The worker stopping event.
        /// </summary>
        public event EventHandler<WorkerStoppingEventArgs> WorkerStopping;

        /// <summary>
        ///     The worker exception event.
        /// </summary>
        public event EventHandler<MessagingExceptionEventArgs> WorkerWarning;

        /// <summary>
        /// Occurs when [subscriber message count].
        /// </summary>
        public event EventHandler<SubscriberMessageCountEventArgs> SubscriberMessageCount;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static ActivityMonitor Instance
        {
            get
            {
                return Monitor;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the listeners.
        /// </summary>
        public IEnumerable<IActivityListener> Listeners { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The bridge close.
        /// </summary>
        /// <param name="bridge">
        /// The bridge.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable BridgeClose(IMessageBusBridge bridge)
        {
            return GetActivityTracker(
                () => this.OnBridgeClosing(bridge, new BridgeClosingEventArgs()), 
                t => this.OnBridgeClosed(bridge, new BridgeClosedEventArgs { Elapsed = t }));
        }

        /// <summary>
        /// The bridge initialize.
        /// </summary>
        /// <param name="bridge">
        /// The bridge.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable BridgeInitialize(IMessageBusBridge bridge, MessageBusBridgeDescription description)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new BridgeInitializingEventArgs { Description = description };
                        this.OnBridgeInitializing(bridge, args);
                    }, 
                t =>
                    {
                        var args = new BridgeInitializedEventArgs { Elapsed = t, Description = description };
                        this.OnBridgeInitialized(bridge, args);
                    });
        }

        /// <summary>
        /// The bridge transfer.
        /// </summary>
        /// <param name="bridge">
        /// The bridge.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable BridgeTransfer(IMessageBusBridge bridge, IMessage message)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new BridgeTransferringEventArgs { Message = message };
                        this.OnBridgeTransferring(bridge, args);
                    }, 
                t =>
                    {
                        var args = new BridgeTransferredEventArgs { Elapsed = t, Message = message };
                        this.OnBridgeTransferred(bridge, args);
                    });
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The type of exception is not known until runtime.")]
        public void Initialize()
        {
            try
            {
                this.Listeners = this.CreateListeners();
                foreach (var listener in this.Listeners)
                {
                    listener.Initialize(this);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        /// <summary>
        /// The message bus close.
        /// </summary>
        /// <param name="messageBus">
        /// The message bus.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable MessageBusClose(IMessageBus messageBus)
        {
            return GetActivityTracker(
                () => this.OnMessageBusClosing(messageBus, new MessageBusClosingEventArgs()), 
                t => this.OnMessageBusClosed(messageBus, new MessageBusClosedEventArgs { Elapsed = t }));
        }

        /// <summary>
        /// The message bus register handler.
        /// </summary>
        /// <param name="messageBus">
        /// The message bus.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable MessageBusRegisterHandler(IMessageBus messageBus, string entity, string name)
        {
            return
                GetActivityTracker(
                    () =>
                        {
                            var args = new MessageBusRegisteringHandlerEventArgs { Entity = entity, Name = name };
                            this.OnMessageBusRegisteringHandler(messageBus, args);
                        }, 
                    t =>
                        {
                            var args = new MessageBusRegisteredHandlerEventArgs
                                           {
                                               Elapsed = t,
                                               Entity = entity,
                                               Name = name
                                           };
                            this.OnMessageBusRegisteredHandler(messageBus, args);
                        });
        }

        /// <summary>
        /// The message bus send.
        /// </summary>
        /// <param name="messageBus">
        /// The message bus.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable MessageBusSend(IMessageBus messageBus, IMessage message, string entity)
        {
            return
                GetActivityTracker(
                    () =>
                        {
                            var args = new MessageBusSendingEventArgs { Message = message, Entity = entity };
                            this.OnMessageBusSending(messageBus, args);
                        }, 
                    t =>
                        {
                            var args = new MessageBusSentEventArgs { Elapsed = t, Message = message, Entity = entity };
                            this.OnMessageBusSent(messageBus, args);
                        });
        }

        /// <summary>
        /// The publisher close.
        /// </summary>
        /// <param name="publisher">
        /// The publisher.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable PublisherClose(IPublisher publisher)
        {
            return GetActivityTracker(
                () => this.OnPublisherClosing(publisher, new PublisherClosingEventArgs()), 
                t => this.OnPublisherClosed(publisher, new PublisherClosedEventArgs { Elapsed = t }));
        }

        /// <summary>
        /// The publisher initialize.
        /// </summary>
        /// <param name="publisher">
        /// The publisher.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable PublisherInitialize(IPublisher publisher, PublisherDescription description)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new PublisherInitializingEventArgs { Description = description };
                        this.OnPublisherInitializing(publisher, args);
                    }, 
                t =>
                    {
                        var args = new PublisherInitializedEventArgs { Elapsed = t, Description = description };
                        this.OnPublisherInitialized(publisher, args);
                    });
        }

        /// <summary>
        /// The publisher send.
        /// </summary>
        /// <param name="publisher">
        /// The publisher.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable PublisherSend(IPublisher publisher, IMessage message)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new PublisherSendingEventArgs { Message = message };
                        this.OnPublisherSending(publisher, args);
                    }, 
                t =>
                    {
                        var args = new PublisherSentEventArgs { Elapsed = t, Message = message };
                        this.OnPublisherSent(publisher, args);
                    });
        }

        /// <summary>
        /// The report bridge exception.
        /// </summary>
        /// <param name="bridge">
        /// The bridge.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="isWarning">
        /// Determines if exception should be treated as warning.
        /// </param>
        public void ReportBridgeException(IMessageBusBridge bridge, Exception exception, bool isWarning)
        {
            if (isWarning)
            {
                this.OnBridgeWarning(bridge, new MessagingExceptionEventArgs { Exception = exception });
            }

            this.OnBridgeException(bridge, new MessagingExceptionEventArgs { Exception = exception });
        }

        /// <summary>
        /// The report message bus exception.
        /// </summary>
        /// <param name="messageBus">
        /// The message bus.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="isWarning">
        /// Determines if exception should be treated as warning.
        /// </param>
        public void ReportMessageBusException(IMessageBus messageBus, Exception exception, bool isWarning)
        {
            if (isWarning)
            {
                this.OnMessageBusWarning(messageBus, new MessagingExceptionEventArgs { Exception = exception });
            }

            this.OnMessageBusException(messageBus, new MessagingExceptionEventArgs { Exception = exception });
        }

        /// <summary>
        /// The report publisher exception.
        /// </summary>
        /// <param name="publisher">
        /// The publisher.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="isWarning">
        /// Determines if exception should be treated as warning.
        /// </param>
        public void ReportPublisherException(IPublisher publisher, Exception exception, bool isWarning)
        {
            if (isWarning)
            {
                this.OnPublisherWarning(publisher, new MessagingExceptionEventArgs { Exception = exception });
            }

            this.OnPublisherException(publisher, new MessagingExceptionEventArgs { Exception = exception });
        }

        /// <summary>
        /// Reports the subscriber exception.
        /// </summary>
        /// <param name="subscriber">
        /// The subscriber.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="isWarning">
        /// Determines if exception should be treated as warning.
        /// </param>
        public void ReportSubscriberException(ISubscriber subscriber, Exception exception, bool isWarning)
        {
            if (isWarning)
            {
                this.OnSubscriberWarning(subscriber, new MessagingExceptionEventArgs { Exception = exception });
            }

            this.OnSubscriberException(subscriber, new MessagingExceptionEventArgs { Exception = exception });
        }

        /// <summary>
        /// The report worker exception.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="isWarning">
        /// Determines if exception should be treated as warning.
        /// </param>
        public void ReportWorkerException(object role, Exception exception, bool isWarning)
        {
            if (isWarning)
            {
                this.OnWorkerWarning(role, new MessagingExceptionEventArgs { Exception = exception });
            }

            this.OnWorkerException(role, new MessagingExceptionEventArgs { Exception = exception });
        }

        /// <summary>
        /// The subscriber close.
        /// </summary>
        /// <param name="subscriber">
        /// The subscriber.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable SubscriberClose(ISubscriber subscriber)
        {
            return GetActivityTracker(
                () => this.OnSubscriberClosing(subscriber, new SubscriberClosingEventArgs()), 
                t => this.OnSubscriberClosed(subscriber, new SubscriberClosedEventArgs { Elapsed = t }));
        }

        /// <summary>
        /// The subscriber initialize.
        /// </summary>
        /// <param name="publisher">
        /// The publisher.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable SubscriberInitialize(ISubscriber publisher, SubscriberDescription description)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new SubscriberInitializingEventArgs { Description = description };
                        this.OnSubscriberInitializing(publisher, args);
                    }, 
                t =>
                    {
                        var args = new SubscriberInitializedEventArgs { Elapsed = t, Description = description };
                        this.OnSubscriberInitialized(publisher, args);
                    });
        }

        /// <summary>
        /// The subscriber receive.
        /// </summary>
        /// <param name="subscriber">
        /// The subscriber.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable SubscriberReceive(ISubscriber subscriber, IMessage message)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new SubscriberReceivingEventArgs { Message = message };
                        this.OnSubscriberReceiving(subscriber, args);
                    }, 
                t =>
                    {
                        var args = new SubscriberReceivedEventArgs { Elapsed = t, Message = message };
                        this.OnSubscriberReceived(subscriber, args);
                    });
        }

        /// <summary>
        /// The worker run.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable WorkerRun(object role)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new WorkerRunningEventArgs();
                        this.OnWorkerRunning(role, args);
                    }, 
                t =>
                    {
                        var args = new WorkerCompletedEventArgs { Elapsed = t };
                        this.OnWorkerCompleted(role, args);
                    });
        }

        /// <summary>
        /// The worker start.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable WorkerStart(object role)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new WorkerStartingEventArgs();
                        this.OnWorkerStarting(role, args);
                    }, 
                t =>
                    {
                        var args = new WorkerStartedEventArgs { Elapsed = t };
                        this.OnWorkerStarted(role, args);
                    });
        }

        /// <summary>
        /// The worker stop.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable WorkerStop(object role)
        {
            return GetActivityTracker(
                () =>
                    {
                        var args = new WorkerStoppingEventArgs();
                        this.OnWorkerStopping(role, args);
                    }, 
                t =>
                    {
                        var args = new WorkerStoppedEventArgs { Elapsed = t };
                        this.OnWorkerStopped(role, args);
                    });
        }

        /// <summary>
        /// Reports the subscription message count.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="args">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        /// <param name="totalMessageCount">The total message count.</param>
        /// <param name="activeMessageCount">The active message count.</param>
        /// <param name="deadLetterMessageCount">The dead letter message count.</param>
        /// <param name="description">The description.</param>
        public void ReportSubscriptionMessageCount(
            ISubscriber subscriber,
            ElapsedEventArgs args,
            long totalMessageCount,
            long activeMessageCount,
            long deadLetterMessageCount,
            SubscriberDescription description)
        {
            if (args != null)
            {
                this.OnSubscriberMessageCount(
                    subscriber,
                    new SubscriberMessageCountEventArgs
                        {
                            SignalTime = args.SignalTime,
                            TotalMessageCount = totalMessageCount,
                            ActiveMessageCount = activeMessageCount,
                            DeadLetterMessageCount = deadLetterMessageCount,
                            SubscriberDescription = description
                        });
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Resolves all the listeners which are configured/registered via Unity
        /// </summary>
        /// <returns>
        ///     Returns IEnumerable of IActivityListener
        /// </returns>
        public IEnumerable<IActivityListener> CreateListeners()
        {
            // UnityContainer container = new UnityContainer();
            // container.RegisterTypes(
            // AllClasses.FromAssembliesInBasePath(),
            // WithMappings.FromMatchingInterface,
            // WithName.TypeName);
            var resolveAll = DependencyResolver.ResolveAll<IActivityListener>();
            return resolveAll ?? new IActivityListener[] { };
        }

        /// <summary>
        /// Gets the disposable activity tracker.
        /// </summary>
        /// <param name="start">
        /// The start method.
        /// </param>
        /// <param name="finish">
        /// The finish method.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/> activity tracker.
        /// </returns>
        private static IDisposable GetActivityTracker(Action start, Action<TimeSpan> finish)
        {
            NotifyListeners(start);
            return new ActivityTracker(span => NotifyListeners(finish, span));
        }

        /// <summary>
        /// Notifies the listeners using the specified callback method.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception types are not known.")]
        private static void NotifyListeners(Action callback)
        {
            try
            {
                callback();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        /// <summary>
        /// Notifies the listeners using the specified callback method and the elapsed <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="elapsed">
        /// The elapsed <see cref="TimeSpan"/>.
        /// </param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The general exception clause is required since the exception types are not known.")]
        private static void NotifyListeners(Action<TimeSpan> callback, TimeSpan elapsed)
        {
            try
            {
                callback(elapsed);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        /// <summary>
        /// The on bridge closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeClosed(object sender, BridgeClosedEventArgs args)
        {
            if (this.BridgeClosed != null)
            {
                this.BridgeClosed(sender, args);
            }
        }

        /// <summary>
        /// The on bridge closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeClosing(object sender, BridgeClosingEventArgs args)
        {
            if (this.BridgeClosing != null)
            {
                this.BridgeClosing(sender, args);
            }
        }

        /// <summary>
        /// The on bridge exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeException(object sender, MessagingExceptionEventArgs args)
        {
            if (this.BridgeException != null)
            {
                this.BridgeException(sender, args);
            }
        }

        /// <summary>
        /// The on bridge initialized.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeInitialized(object sender, BridgeInitializedEventArgs args)
        {
            if (this.BridgeInitialized != null)
            {
                this.BridgeInitialized(sender, args);
            }
        }

        /// <summary>
        /// The on bridge initializing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeInitializing(object sender, BridgeInitializingEventArgs args)
        {
            if (this.BridgeInitializing != null)
            {
                this.BridgeInitializing(sender, args);
            }
        }

        /// <summary>
        /// The on bridge transferred.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeTransferred(object sender, BridgeTransferredEventArgs args)
        {
            if (this.BridgeTransferred != null)
            {
                this.BridgeTransferred(sender, args);
            }
        }

        /// <summary>
        /// The on bridge transferring.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeTransferring(object sender, BridgeTransferringEventArgs args)
        {
            if (this.BridgeTransferring != null)
            {
                this.BridgeTransferring(sender, args);
            }
        }

        /// <summary>
        /// The on bridge exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnBridgeWarning(object sender, MessagingExceptionEventArgs args)
        {
            if (this.BridgeWarning != null)
            {
                this.BridgeWarning(sender, args);
            }
        }

        /// <summary>
        /// The on message bus closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusClosed(object sender, MessageBusClosedEventArgs args)
        {
            if (this.MessageBusClosed != null)
            {
                this.MessageBusClosed(sender, args);
            }
        }

        /// <summary>
        /// The on message bus closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusClosing(object sender, MessageBusClosingEventArgs args)
        {
            if (this.MessageBusClosing != null)
            {
                this.MessageBusClosing(sender, args);
            }
        }

        /// <summary>
        /// The on message bus exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusException(object sender, MessagingExceptionEventArgs args)
        {
            if (this.MessageBusException != null)
            {
                this.MessageBusException(sender, args);
            }
        }

        /// <summary>
        /// The on message bus registered handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusRegisteredHandler(object sender, MessageBusRegisteredHandlerEventArgs args)
        {
            if (this.MessageBusRegisteredHandler != null)
            {
                this.MessageBusRegisteredHandler(sender, args);
            }
        }

        /// <summary>
        /// The on message bus registering handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusRegisteringHandler(object sender, MessageBusRegisteringHandlerEventArgs args)
        {
            if (this.MessageBusRegisteringHandler != null)
            {
                this.MessageBusRegisteringHandler(sender, args);
            }
        }

        /// <summary>
        /// The on message bus sending.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusSending(object sender, MessageBusSendingEventArgs args)
        {
            if (this.MessageBusSending != null)
            {
                this.MessageBusSending(sender, args);
            }
        }

        /// <summary>
        /// The on message bus sent.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusSent(object sender, MessageBusSentEventArgs args)
        {
            if (this.MessageBusSent != null)
            {
                this.MessageBusSent(sender, args);
            }
        }

        /// <summary>
        /// The on message bus exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnMessageBusWarning(object sender, MessagingExceptionEventArgs args)
        {
            if (this.MessageBusWarning != null)
            {
                this.MessageBusWarning(sender, args);
            }
        }

        /// <summary>
        /// The on publisher closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherClosed(object sender, PublisherClosedEventArgs args)
        {
            if (this.PublisherClosed != null)
            {
                this.PublisherClosed(sender, args);
            }
        }

        /// <summary>
        /// The on publisher closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherClosing(object sender, PublisherClosingEventArgs args)
        {
            if (this.PublisherClosing != null)
            {
                this.PublisherClosing(sender, args);
            }
        }

        /// <summary>
        /// The on publisher exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherException(object sender, MessagingExceptionEventArgs args)
        {
            if (this.PublisherException != null)
            {
                this.PublisherException(sender, args);
            }
        }

        /// <summary>
        /// The on publisher initialized.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherInitialized(object sender, PublisherInitializedEventArgs args)
        {
            if (this.PublisherInitialized != null)
            {
                this.PublisherInitialized(sender, args);
            }
        }

        /// <summary>
        /// The on publisher initializing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherInitializing(object sender, PublisherInitializingEventArgs args)
        {
            if (this.PublisherInitializing != null)
            {
                this.PublisherInitializing(sender, args);
            }
        }

        /// <summary>
        /// The on publisher sending.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherSending(object sender, PublisherSendingEventArgs args)
        {
            if (this.PublisherSending != null)
            {
                this.PublisherSending(sender, args);
            }
        }

        /// <summary>
        /// The on publisher sent.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherSent(object sender, PublisherSentEventArgs args)
        {
            if (this.PublisherSent != null)
            {
                this.PublisherSent(sender, args);
            }
        }

        /// <summary>
        /// The on publisher exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnPublisherWarning(object sender, MessagingExceptionEventArgs args)
        {
            if (this.PublisherWarning != null)
            {
                this.PublisherWarning(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberClosed(object sender, SubscriberClosedEventArgs args)
        {
            if (this.SubscriberClosed != null)
            {
                this.SubscriberClosed(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberClosing(object sender, SubscriberClosingEventArgs args)
        {
            if (this.SubscriberClosing != null)
            {
                this.SubscriberClosing(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberException(object sender, MessagingExceptionEventArgs args)
        {
            if (this.SubscriberException != null)
            {
                this.SubscriberException(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber initialized.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberInitialized(object sender, SubscriberInitializedEventArgs args)
        {
            if (this.SubscriberInitialized != null)
            {
                this.SubscriberInitialized(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber initializing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberInitializing(object sender, SubscriberInitializingEventArgs args)
        {
            if (this.SubscriberInitializing != null)
            {
                this.SubscriberInitializing(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber received.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberReceived(object sender, SubscriberReceivedEventArgs args)
        {
            if (this.SubscriberReceived != null)
            {
                this.SubscriberReceived(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber receiving.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberReceiving(object sender, SubscriberReceivingEventArgs args)
        {
            if (this.SubscriberReceiving != null)
            {
                this.SubscriberReceiving(sender, args);
            }
        }

        /// <summary>
        /// The on subscriber exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnSubscriberWarning(object sender, MessagingExceptionEventArgs args)
        {
            if (this.SubscriberWarning != null)
            {
                this.SubscriberWarning(sender, args);
            }
        }

        /// <summary>
        /// The on worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerCompleted(object sender, WorkerCompletedEventArgs args)
        {
            if (this.WorkerCompleted != null)
            {
                this.WorkerCompleted(sender, args);
            }
        }

        /// <summary>
        /// The on worker exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerException(object sender, MessagingExceptionEventArgs args)
        {
            if (this.WorkerException != null)
            {
                this.WorkerException(sender, args);
            }
        }

        /// <summary>
        /// The on worker running.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerRunning(object sender, WorkerRunningEventArgs args)
        {
            if (this.WorkerRunning != null)
            {
                this.WorkerRunning(sender, args);
            }
        }

        /// <summary>
        /// The on worker started.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerStarted(object sender, WorkerStartedEventArgs args)
        {
            if (this.WorkerStarted != null)
            {
                this.WorkerStarted(sender, args);
            }
        }

        /// <summary>
        /// The on worker starting.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerStarting(object sender, WorkerStartingEventArgs args)
        {
            if (this.WorkerStarting != null)
            {
                this.WorkerStarting(sender, args);
            }
        }

        /// <summary>
        /// The on worker stopped.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerStopped(object sender, WorkerStoppedEventArgs args)
        {
            if (this.WorkerStopped != null)
            {
                this.WorkerStopped(sender, args);
            }
        }

        /// <summary>
        /// The on worker stopping.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerStopping(object sender, WorkerStoppingEventArgs args)
        {
            if (this.WorkerStopping != null)
            {
                this.WorkerStopping(sender, args);
            }
        }

        /// <summary>
        /// The on worker exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The eventArgs.
        /// </param>
        private void OnWorkerWarning(object sender, MessagingExceptionEventArgs args)
        {
            if (this.WorkerWarning != null)
            {
                this.WorkerWarning(sender, args);
            }
        }

        /// <summary>
        /// Called when [subscriber message count].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The arguments.</param>
        private void OnSubscriberMessageCount(ISubscriber sender, SubscriberMessageCountEventArgs eventArgs)
        {
            if (this.SubscriberMessageCount != null)
            {
                this.SubscriberMessageCount(sender, eventArgs);
            }
        }

        #endregion
    }
}