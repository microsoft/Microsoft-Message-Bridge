// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerEventTraceWriter.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring.EventTracing
{
    using Microsoft.Diagnostics.Tracing;

    /// <summary>
    ///     The worker event trace writer.
    /// </summary>
    public sealed class WorkerEventTraceWriter : EventSource
    {
        #region Constants

        /// <summary>
        ///     The base event code
        /// </summary>
        private const int BaseEventCode = 10000;

        /// <summary>
        ///     The completed event code
        /// </summary>
        private const int CompletedEventCode = BaseEventCode + 4;

        /// <summary>
        ///     The exception event code
        /// </summary>
        private const int ExceptionEventCode = BaseEventCode + 0;

        /// <summary>
        ///     The running event code
        /// </summary>
        private const int RunningEventCode = BaseEventCode + 3;

        /// <summary>
        ///     The started event code
        /// </summary>
        private const int StartedEventCode = BaseEventCode + 2;

        /// <summary>
        ///     The starting event code
        /// </summary>
        private const int StartingEventCode = BaseEventCode + 1;

        /// <summary>
        ///     The stopped event code
        /// </summary>
        private const int StoppedEventCode = BaseEventCode + 6;

        /// <summary>
        ///     The stopping event code
        /// </summary>
        private const int StoppingEventCode = BaseEventCode + 5;

        /// <summary>
        ///     The warning event code
        /// </summary>
        private const int WarningEventCode = BaseEventCode + 10;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Indicates that the specified worker as completed.
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
        /// <param name="worker">
        /// The worker.
        /// </param>
        /// <param name="elapsed">
        /// The elapsed time.
        /// </param>
        [Event(CompletedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Completed ({5} seconds).")]
        public void Completed(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string worker, 
            string elapsed)
        {
            this.WriteEvent(
                CompletedEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                worker, 
                elapsed);
        }

        /// <summary>
        /// Writes the worker error event message.
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
        /// <param name="worker">
        /// The worker.
        /// </param>
        /// <param name="errorDetails">
        /// The error.
        /// </param>
        [Event(ExceptionEventCode, Level = EventLevel.Critical, 
            Message =
                "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker: {4}] Exception: {5}")]
        public void Error(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string worker, 
            string errorDetails)
        {
            this.WriteEvent(
                ExceptionEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                worker, 
                errorDetails);
        }

        /// <summary>
        /// Writes the worker running event message.
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
        /// <param name="sender">
        /// The sender.
        /// </param>
        [Event(RunningEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Running.")]
        public void Running(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string sender)
        {
            this.WriteEvent(RunningEventCode, machineName, deploymentId, roleInstanceName, roleInstanceId, sender);
        }

        /// <summary>
        /// Writes the worker started event message.
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
        [Event(StartedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Started ({5} seconds).")]
        public void Started(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string seconds)
        {
            this.WriteEvent(
                StartedEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                seconds);
        }

        /// <summary>
        /// Writes the worker starting event message.
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
        [Event(StartingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Starting.")]
        public void Starting(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name)
        {
            this.WriteEvent(StartingEventCode, machineName, deploymentId, roleInstanceName, roleInstanceId, name);
        }

        /// <summary>
        /// Writes the worker stopped event message.
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
        [Event(StoppedEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Stopped ({5} seconds).")]
        public void Stopped(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name, 
            string seconds)
        {
            this.WriteEvent(
                StoppedEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                name, 
                seconds);
        }

        /// <summary>
        /// Writes the worker stopping event message.
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
        [Event(StoppingEventCode, Level = EventLevel.Informational, 
            Message = "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker {4}] Stopping.")]
        public void Stopping(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string name)
        {
            this.WriteEvent(StoppingEventCode, machineName, deploymentId, roleInstanceName, roleInstanceId, name);
        }

        /// <summary>
        /// Writes the worker warning event message.
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
        /// <param name="worker">
        /// The worker.
        /// </param>
        /// <param name="error">
        /// The error.
        /// </param>
        [Event(WarningEventCode, Level = EventLevel.Warning, 
            Message =
                "[Machine Name: {0}][DeploymentId: {1}][Role: {2}][Role Instance Id: {3}][Worker: {4}] Exception: {5}")]
        public void Warning(
            string machineName, 
            string deploymentId, 
            string roleInstanceName, 
            string roleInstanceId, 
            string worker, 
            string error)
        {
            this.WriteEvent(
                WarningEventCode, 
                machineName, 
                deploymentId, 
                roleInstanceName, 
                roleInstanceId, 
                worker, 
                error);
        }

        #endregion
    }
}