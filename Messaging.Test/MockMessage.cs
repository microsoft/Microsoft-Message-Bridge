// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockMessage.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     A mock <see cref="IMessage" />
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class MockMessage : IMessage
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the correlation key. The correlation key should be the <see cref="MessageKey" /> of a previous
        ///     message.
        /// </summary>
        public string CorrelationKey { get; set; }

        /// <summary>
        ///     Gets or sets the message. The message is a JSON-encoded object being sent over the <see cref="MessageBus" />.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the message key associated with the message.
        /// </summary>
        public string MessageKey { get; set; }

        /// <summary>
        ///     Gets or sets the partition key associated with the message.
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        ///     Gets or sets the custom properties associated with the message.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Gets the name of the message
        /// </summary>
        public string Name
        {
            get
            {
                return this.ExtractName();
            }
        }

        /// <summary>
        /// Extracts the message name from inside JSON body
        /// </summary>
        /// <returns>Name of the message</returns>
        private string ExtractName()
        {
            string name = string.Empty;
            try
            {
                var obj = (JObject)JsonConvert.DeserializeObject(this.Message);
                name = obj["Name"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return name;
        }

        #endregion
    }
}