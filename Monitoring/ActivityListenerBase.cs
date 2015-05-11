// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityListenerBase.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// <summary>
//   The activity listener base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Monitoring
{
    using System;

    using Microsoft.WindowsAzure.ServiceRuntime;

    /// <summary>
    ///     The activity listener base.
    /// </summary>
    public abstract class ActivityListenerBase : IActivityListener
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the deployment id.
        /// </summary>
        public string DeploymentId { get; set; }

        /// <summary>
        ///     Gets or sets the machine name.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the role instance id.
        /// </summary>
        public string RoleInstanceId { get; set; }

        /// <summary>
        ///     Gets or sets the role instance name.
        /// </summary>
        public string RoleInstanceName { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The initialize method.
        /// </summary>
        /// <param name="monitor">
        /// The monitor.
        /// </param>
        public virtual void Initialize(IActivityMonitor monitor)
        {
            this.MachineName = Environment.MachineName;
            this.DeploymentId = string.Empty;
            this.RoleInstanceName = string.Empty;
            this.RoleInstanceId = string.Empty;

            if (RoleEnvironment.CurrentRoleInstance == null)
            {
                return;
            }

            this.DeploymentId = RoleEnvironment.DeploymentId;
            this.RoleInstanceId = RoleEnvironment.CurrentRoleInstance.Id;

            if (RoleEnvironment.CurrentRoleInstance.Role != null)
            {
                this.RoleInstanceName = RoleEnvironment.CurrentRoleInstance.Role.Name;
            }
        }

        #endregion
    }
}