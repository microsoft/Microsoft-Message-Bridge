// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriberEventTraceWriter.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.EventTracing
{
    using Microsoft.Diagnostics.Tracing;

    /// <summary>
    ///     The subscriber event trace writer.
    /// </summary>
    [EventSource(Name = "MessageBridge-Messaging-Subscriber", Guid = "3ED51906-C9D5-4C4D-B5FE-BD9E46557661")]
    public sealed class SubscriberEventTraceWriter : EventSource
    {
        #region Constants

        /// <summary>
        ///     The base event code
        /// </summary>
        private const int BaseEventCode = 22000;

        /// <summary>
        ///     The closed event code
        /// </summary>
        private const int ClosedEventCode = BaseEventCode + 6;

        /// <summary>
        ///     The closing event code
        /// </summary>
        private const int ClosingEventCode = BaseEventCode + 5;

        /// <summary>
        ///     The exception event code
        /// </summary>
        private const int ExceptionEventCode = BaseEventCode + 0;

        /// <summary>
        ///     The initialized event code
        /// </summary>
        private const int InitializedEventCode = BaseEventCode + 2;

        /// <summary>
        ///     The initializing event code
        /// </summary>
        private const int InitializingEventCode = BaseEventCode + 1;

        /// <summary>
        ///     The received event code
        /// </summary>
        private const int ReceivedEventCode = BaseEventCode + 4;

        /// <summary>
        ///     The receiving event code
        /// </summary>
        private const int ReceivingEventCode = BaseEventCode + 3;

        /// <summary>
        ///     The warning event code
        /// </summary>
        private const int WarningEventCode = BaseEventCode + 10;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Writes the subscriber closed event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        [Event(ClosedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Closed ({5} seconds).")]
        public void Closed(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string seconds)
        {
            this.WriteEvent(ClosedEventCode, machineName, deploymentId, roleInstanceName, roleInstanceId, name, seconds);
        }

        /// <summary>
        /// Writes the subscriber closing event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        [Event(ClosingEventCode, Level = EventLevel.Informational, 
            Message =
                "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Closing.")]
        public void Closing(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name)
        {
            this.WriteEvent(ClosingEventCode, machineName, deploymentId, roleInstanceName, roleInstanceId, name);
        }

        /// <summary>
        /// Writes the subscriber error event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [Event(ExceptionEventCode, Level = EventLevel.Critical, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber: {4} Exception: {5}")]
        public void Error(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string exception)
        {
            this.WriteEvent(
                ExceptionEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                exception);
        }

        /// <summary>
        /// Writes the subscriber initialized event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="subscriberName">
        /// Name of the subscriber.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        [Event(InitializedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Initialized {5} on {6} ({7} seconds).")]
        public void Initialized(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string subscriberName, 
            string entity, 
            string seconds)
        {
            this.WriteEvent(
                InitializedEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                subscriberName, 
                entity, 
                seconds);
        }

        /// <summary>
        /// Writes the subscriber initializing event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="subscriberName">
        /// Name of the subscriber.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="cs">
        /// The connection string.
        /// </param>
        [Event(InitializingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Initializing {5} on {6} ({7}).")]
        public void Initializing(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string subscriberName, 
            string entity, 
            string cs)
        {
            this.WriteEvent(
                InitializingEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                subscriberName, 
                entity, 
                cs);
        }

        /// <summary>
        /// Writes the message received event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="messageKey">
        /// The message key.
        /// </param>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        [Event(ReceivedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Received message {5} ({6} seconds).")]
        public void Received(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string messageKey, 
            string seconds)
        {
            this.WriteEvent(
                ReceivedEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                messageKey, 
                seconds);
        }

        /// <summary>
        /// Writes the message receiving event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="messageKey">
        /// The message key.
        /// </param>
        [Event(ReceivingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber {4}] Receiving message {5}.")]
        public void Receiving(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string messageKey)
        {
            this.WriteEvent(
                ReceivingEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                messageKey);
        }

        /// <summary>
        /// Writes the subscriber warning event message.
        /// </summary>
        /// <param name="machineName">
        /// The machine Name.
        /// </param>
        /// <param name="deploymentId">
        /// The deployment Id.
        /// </param>
        /// <param name="roleInstanceName">
        /// The role Instance Name.
        /// </param>
        /// <param name="roleInstanceId">
        /// The role Instance Id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [Event(WarningEventCode, Level = EventLevel.Warning, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Subscriber: {4} Exception: {5}")]
        public void Warning(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string exception)
        {
            this.WriteEvent(
                WarningEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                exception);
        }

        #endregion
    }
}