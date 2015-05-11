// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusEventTraceWriter.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.EventTracing
{
    using Microsoft.Diagnostics.Tracing;

    /// <summary>
    ///     The message bus event trace writer.
    /// </summary>
    [EventSource(Name = "MessageBridge-Messaging-Bus", Guid = "E6F2F53B-D24C-4AEC-A387-EFAECAFD767D")]
    public sealed class MessageBusEventTraceWriter : EventSource
    {
        #region Constants

        /// <summary>
        ///     The base event code
        /// </summary>
        private const int BaseEventCode = 20000;

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
        ///     The registered event code
        /// </summary>
        private const int RegisteredEventCode = BaseEventCode + 2;

        /// <summary>
        ///     The registering event code
        /// </summary>
        private const int RegisteringEventCode = BaseEventCode + 1;

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
        /// Writes the bus closed event message.
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
        /// The elapsed seconds.
        /// </param>
        [Event(ClosedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Closed ({5} seconds).")]
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
        /// Writes the bus closing event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Closing.")]
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
        /// Writes the bus error event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4} Exception: {5}")
        ]
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
        /// Writes the message sending event message.
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
        /// <param name="busName">
        /// Name of the bus.
        /// </param>
        /// <param name="messageKey">
        /// The message key.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        [Event(SendingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Sending message {5} to {6}.")]
        public void MessageSending(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string busName, 
            string messageKey, 
            string entity)
        {
            this.WriteEvent(
                SendingEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                busName, 
                messageKey, 
                entity);
        }

        /// <summary>
        /// Messages the sent.
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
        /// <param name="busName">
        /// Name of the bus.
        /// </param>
        /// <param name="messageKey">
        /// The message key.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        [Event(SentEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Sent message {5} to {6} ({7} seconds).")]
        public void MessageSent(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string busName, 
            string messageKey, 
            string entity, 
            string seconds)
        {
            this.WriteEvent(
                SentEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                busName, 
                messageKey, 
                entity, 
                seconds);
        }

        /// <summary>
        /// Writes the bus handler registered event message.
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
        /// <param name="busName">
        /// The name.
        /// </param>
        /// <param name="handlerName">
        /// Name of the bus.
        /// </param>
        /// <param name="busEntity">
        /// The bus entity.
        /// </param>
        /// <param name="seconds">
        /// The seconds.
        /// </param>
        [Event(RegisteredEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Registered handler {5} for {6} ({7}).")]
        public void Registered(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string busName, 
            string handlerName, 
            string busEntity, 
            string seconds)
        {
            this.WriteEvent(
                RegisteredEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                busName, 
                handlerName, 
                busEntity, 
                seconds);
        }

        /// <summary>
        /// Writes the bus handler registering event message.
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
        /// <param name="busName">
        /// Name of the bus.
        /// </param>
        /// <param name="handlerName">
        /// Name of the handler.
        /// </param>
        /// <param name="busEntity">
        /// The bus entity.
        /// </param>
        [Event(RegisteringEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Registering handler {5} for {6}.")]
        public void Registering(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string busName, 
            string handlerName, 
            string busEntity)
        {
            this.WriteEvent(
                RegisteringEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                busName, 
                handlerName, 
                busEntity);
        }

        /// <summary>
        /// Writes the bus error warning message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bus {4}] Exception: {5}")]
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