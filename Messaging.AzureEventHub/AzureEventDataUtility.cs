// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureEventDataUtility.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.AzureEventHub
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    ///     A set of utility conversation methods for event data and messages.
    /// </summary>
    internal static class AzureEventDataUtility
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts the <see cref="IMessage"/> instance to an <see cref="EventData"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="EventData"/>
        /// </returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "The event data instance will be disposed by an upstream caller.")]
        public static EventData ToEventData(this IMessage message)
        {
            var eventData = new EventData(Encoding.UTF8.GetBytes(message.Message))
                                {
                                    PartitionKey =
                                        GetPartitionKey(message)
                                };

            foreach (var entry in message.Properties)
            {
                eventData.Properties.Add(entry.Key, entry.Value);
            }

            return eventData;
        }

        /// <summary>
        /// Converts the <see cref="EventData"/> to an <see cref="IMessage"/> instance.
        /// </summary>
        /// <param name="eventData">
        /// The event data.
        /// </param>
        /// <returns>
        /// The <see cref="IMessage"/> instance
        /// </returns>
        public static IMessage ToMessage(this EventData eventData)
        {
            var message = new EventMessage
                              {
                                  Message = Encoding.UTF8.GetString(eventData.GetBytes()), 
                                  PartitionKey = eventData.PartitionKey
                              };

            foreach (var entry in eventData.Properties)
            {
                message.Properties.Add(entry.Key, entry.Value);
            }

            return message;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the partition key.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The partition key for the message.
        /// </returns>
        private static string GetPartitionKey(IMessage message)
        {
            var pk = message.PartitionKey;
            return string.IsNullOrWhiteSpace(pk) ? Guid.NewGuid().ToString("N") : pk;
        }

        #endregion
    }
}