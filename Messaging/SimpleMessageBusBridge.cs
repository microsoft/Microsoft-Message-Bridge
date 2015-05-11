// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleMessageBusBridge.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.MessageBridge.Monitoring;

    /// <summary>
    ///     Represents a simplex connection between two <see cref="MessageBus" /> instances. The two instances must use the
    ///     same message format. The two instances do not need to use the same underlying mechanism.
    /// </summary>
    /// <remarks>
    ///     Duplex communication over a single bridge is not supported. If this is required then use two bridges.
    /// </remarks>
    public class SimpleMessageBusBridge : IMessageBusBridge
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the target entity.
        /// </summary>
        protected string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the source message bus.
        /// </summary>
        protected IMessageBus Source { get; set; }

        /// <summary>
        ///     Gets or sets the target message bus.
        /// </summary>
        protected IMessageBus Target { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the message bridge and the underlying <see cref="MessageBus" /> instances asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the close operation.</returns>
        public virtual async Task CloseAsync()
        {
            using (ActivityMonitor.Instance.BridgeClose(this))
            {
                await Task.WhenAll(this.Source.CloseAsync(), this.Target.CloseAsync());
            }
        }

        /// <summary>
        /// Initializes the message bus bridge connection asynchronously.
        /// </summary>
        /// <param name="description">
        /// The bridge description.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the initialization operation.
        /// </returns>
        public virtual async Task InitializeAsync(MessageBusBridgeDescription description)
        {
            using (ActivityMonitor.Instance.BridgeInitialize(this, description))
            {
                if (description == null)
                {
                    var exception = new ArgumentNullException("description");
                    ActivityMonitor.Instance.ReportBridgeException(this, exception, false);
                    throw exception;
                }

                this.Source = description.SourceBus;
                this.Target = description.TargetBus;
                this.Entity = description.TargetEntity;

                await
                    this.Source.RegisterHandlerAsync(
                        description.SourceEntity,
                        description.BridgeName,
                        this.OnSourceMessageArrived);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when a source message arrives.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the bridge operation.
        /// </returns>
        protected virtual async Task OnSourceMessageArrived(IMessage message)
        {
            using (ActivityMonitor.Instance.BridgeTransfer(this, message))
            {
                var messages = ProcessMessage(message);
                foreach(var msg in messages)
                {
                    await this.Target.SendAsync(this.Entity,msg );
                }
            }
        }

        public virtual System.Collections.Generic.IEnumerable<IMessage> ProcessMessage(IMessage Message)
        {
            
            var Msgs = new List<IMessage>();
            Msgs.Add(Message);
            return Msgs;
        
        }
        // TODO We use an abstract class and method here to allow the user to iharit the 
        // base class and overide the message processing method.   
        // You will need to impliment your own ProcessMessage if you want to modify the message
        #endregion
    }
}