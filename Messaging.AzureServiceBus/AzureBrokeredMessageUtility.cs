// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureBrokeredMessageUtility.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureServiceBus
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    ///     Implements utility conversion message on <see cref="BrokeredMessage" />.
    /// </summary>
    public static class AzureBrokeredMessageUtility
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts the <see cref="IMessage"/> instance to a <see cref="BrokeredMessage"/>.
        /// </summary>
        /// <param name="message">
        /// The <see cref="IMessage"/> instance.
        /// </param>
        /// <returns>
        /// The <see cref="BrokeredMessage"/>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when the message is null.
        /// </exception>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "The brokered message instance will be disposed by an upstream caller.")]
        public static BrokeredMessage ToBrokeredMessage(this IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            var brokeredMessage = new BrokeredMessage(message.Message)
                                      {
                                          CorrelationId = message.CorrelationKey, 
                                          MessageId = message.MessageKey, 
                                          PartitionKey = message.PartitionKey
                                      };

            foreach (var entry in message.Properties)
            {
                brokeredMessage.Properties.Add(entry.Key, entry.Value);
            }

            return brokeredMessage;
        }

        /// <summary>
        /// Converts a <see cref="BrokeredMessage"/> to an <see cref="IMessage"/>.
        /// </summary>
        /// <param name="brokeredMessage">
        /// The <see cref="BrokeredMessage"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IMessage"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Occurs when the <see cref="BrokeredMessage"/> is null.
        /// </exception>
        public static IMessage ToMessage(this BrokeredMessage brokeredMessage)
        {
            if (brokeredMessage == null)
            {
                throw new ArgumentNullException("brokeredMessage");
            }

            var azureMessage = new EventMessage
                                   {
                                       CorrelationKey = brokeredMessage.CorrelationId,
                                       Message = brokeredMessage.GetBody<string>(),
                                       MessageKey = brokeredMessage.MessageId, 
                                       PartitionKey = brokeredMessage.PartitionKey
                                   };

            foreach (var entry in brokeredMessage.Properties)
            {
                azureMessage.Properties.Add(entry.Key, entry.Value);
            }

            return azureMessage;
        }

        #endregion
    }
}