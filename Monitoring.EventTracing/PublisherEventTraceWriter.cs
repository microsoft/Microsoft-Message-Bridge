// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublisherEventTraceWriter.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.EventTracing
{
    using Microsoft.Diagnostics.Tracing;

    /// <summary>
    ///     The publisher event trace writer.
    /// </summary>
    [EventSource(Name = "MessageBridge-Messaging-Publisher", Guid = "3E652741-2827-499F-A734-FEDF910D7704")]
    public sealed class PublisherEventTraceWriter : EventSource
    {
        #region Constants

        /// <summary>
        ///     The base event code
        /// </summary>
        private const int BaseEventCode = 21000;

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
        ///     The sending event code
        /// </summary>
        private const int SendingEventCode = BaseEventCode + 3;

        /// <summary>
        ///     The sent event code
        /// </summary>
        private const int SentEventCode = BaseEventCode + 4;

        /// <summary>
        ///     The warning event code
        /// </summary>
        private const int WarningEventCode = BaseEventCode + 10;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Writes the publisher closed event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Closed ({5} seconds).")]
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
        /// Writes the publisher closing event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Closing.")]
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
        /// Writes the publisher error event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher: {4} Exception: {5}")]
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
        /// Writes the publisher initialized event message.
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
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        [Event(InitializedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Initialized {5} ({6} seconds).")]
        public void Initialized(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
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
                entity, 
                seconds);
        }

        /// <summary>
        /// Writes the publisher initializing event message.
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
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="cs">
        /// The cs.
        /// </param>
        [Event(InitializingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Initializing {5} ({6}).")]
        public void Initializing(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
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
                entity, 
                cs);
        }

        /// <summary>
        /// Writes the publisher sending event message.
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
        [Event(SendingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Sending message {5}.")]
        public void Sending(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string messageKey)
        {
            this.WriteEvent(
                SendingEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                messageKey);
        }

        /// <summary>
        /// Writes the message sent event message.
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
        [Event(SentEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher {4}] Sent message {5} ({6} seconds).")]
        public void Sent(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string messageKey, 
            string seconds)
        {
            this.WriteEvent(
                SentEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                messageKey, 
                seconds);
        }

        /// <summary>
        /// Writes the publisher warning event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Publisher: {4} Exception: {5}")]
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