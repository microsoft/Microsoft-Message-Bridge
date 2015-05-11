// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BridgeEventTraceWriter.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.EventTracing
{
    using Microsoft.Diagnostics.Tracing;

    /// <summary>
    ///     The bridge event trace writer.
    /// </summary>
    [EventSource(Name = "MessageBridge-Messaging-Bridge", Guid = "E4C763C2-575D-4D72-97E4-BCD06B54AD7C")]
    public sealed class BridgeEventTraceWriter : EventSource
    {
        #region Constants

        /// <summary>
        ///     The base event code
        /// </summary>
        private const int BaseEventCode = 23000;

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
        ///     The transferred event code
        /// </summary>
        private const int TransferredEventCode = BaseEventCode + 4;

        /// <summary>
        ///     The transferring event code
        /// </summary>
        private const int TransferringEventCode = BaseEventCode + 3;

        /// <summary>
        ///     The warning event code
        /// </summary>
        private const int WarningEventCode = BaseEventCode + 10;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Writes the bridge closed event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Closed ({5} seconds).")]
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
        /// Writes the bridge closing event message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Closing.")]
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
        /// Writes a bridge error message.
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
            Message =
                "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4} Exception: {5}")]
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
        /// Writes the initialized event message.
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
        [Event(InitializedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Initialized ({5} seconds).")]
        public void Initialized(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string seconds)
        {
            this.WriteEvent(
                InitializedEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                seconds);
        }

        /// <summary>
        /// Writes the bridge initializing event message.
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
        [Event(InitializingEventCode, Level = EventLevel.Informational, 
            Message =
                "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Initializing.")]
        public void Initializing(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name)
        {
            this.WriteEvent(InitializingEventCode, machineName, deploymentId, roleInstanceName, roleInstanceId, name);
        }

        /// <summary>
        /// Writes a bridge message transferred event message.
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
        [Event(TransferredEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Transferred message {5} ({6} seconds).")]
        public void Transferred(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string messageKey, 
            string seconds)
        {
            this.WriteEvent(
                TransferredEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                messageKey, 
                seconds);
        }

        /// <summary>
        /// Writes a bridge message transferring event message.
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
        [Event(TransferringEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Bridge {4}] Transferring message {5}.")]
        public void Transferring(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string messageKey)
        {
            this.WriteEvent(
                TransferringEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                messageKey);
        }

        /// <summary>
        /// Writes a bridge error message.
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
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}] Exception: {4}")]
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