// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColoredConsoleTraceListener.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Traces colored messages to the console.
    /// </summary>
    public class ColoredConsoleTraceListener : ConsoleTraceListener
    {
        #region Public Methods and Operators

        /// <summary>
        /// Emits an error message.
        /// </summary>
        /// <param name="message">
        /// A message to emit.
        /// </param>
        public override void Fail(string message)
        {
            using (GetColorContext(TraceEventType.Error))
            {
                base.Fail(message);
            }
        }

        /// <summary>
        /// Emits an error message and a detailed error message.
        /// </summary>
        /// <param name="message">
        /// A message to emit.
        /// </param>
        /// <param name="detailMessage">
        /// A detailed message to emit.
        /// </param>
        public override void Fail(string message, string detailMessage)
        {
            using (GetColorContext(TraceEventType.Error))
            {
                base.Fail(message, detailMessage);
            }
        }

        /// <summary>
        /// Writes trace information, a data object and event information.
        /// </summary>
        /// <param name="eventCache">
        /// A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">
        /// A numeric identifier for the event.
        /// </param>
        /// <param name="data">
        /// The trace data to emit.
        /// </param>
        public override void TraceData(
            TraceEventCache eventCache, 
            string source, 
            TraceEventType eventType, 
            int id, 
            object data)
        {
            using (GetColorContext(eventType))
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// Writes trace information, an array of data objects and event information.
        /// </summary>
        /// <param name="eventCache">
        /// A <see cref="T:System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and
        ///     stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the <see cref="T:System.Diagnostics.TraceEventType"/> values specifying the type of event that has caused
        ///     the trace.
        /// </param>
        /// <param name="id">
        /// A numeric identifier for the event.
        /// </param>
        /// <param name="data">
        /// An array of objects to emit as data.
        /// </param>
        public override void TraceData(
            TraceEventCache eventCache, 
            string source, 
            TraceEventType eventType, 
            int id, 
            params object[] data)
        {
            using (GetColorContext(eventType))
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// Writes trace and event information.
        /// </summary>
        /// <param name="eventCache">
        /// A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">
        /// A numeric identifier for the event.
        /// </param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            using (GetColorContext(eventType))
            {
                base.TraceEvent(eventCache, source, eventType, id);
            }
        }

        /// <summary>
        /// Writes trace information, a formatted array of objects and event information.
        /// </summary>
        /// <param name="eventCache">
        /// A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">
        /// A numeric identifier for the event.
        /// </param>
        /// <param name="format">
        /// A format string that contains zero or more format items, which correspond to objects in the
        ///     <paramref name="args"/> array.
        /// </param>
        /// <param name="args">
        /// An object array containing zero or more objects to format.
        /// </param>
        public override void TraceEvent(
            TraceEventCache eventCache, 
            string source, 
            TraceEventType eventType, 
            int id, 
            string format, 
            params object[] args)
        {
            using (GetColorContext(eventType))
            {
                base.TraceEvent(eventCache, source, eventType, id, format, args);
            }
        }

        /// <summary>
        /// Writes trace information, a message, and event information.
        /// </summary>
        /// <param name="eventCache">
        /// A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">
        /// A numeric identifier for the event.
        /// </param>
        /// <param name="message">
        /// A message to write.
        /// </param>
        public override void TraceEvent(
            TraceEventCache eventCache, 
            string source, 
            TraceEventType eventType, 
            int id, 
            string message)
        {
            using (GetColorContext(eventType))
            {
                base.TraceEvent(eventCache, source, eventType, id, message);
            }
        }

        /// <summary>
        /// Writes trace information, a message, a related activity identity and event information.
        /// </summary>
        /// <param name="eventCache">
        /// A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace
        ///     information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="id">
        /// A numeric identifier for the event.
        /// </param>
        /// <param name="message">
        /// A message to write.
        /// </param>
        /// <param name="relatedActivityId">
        /// A <see cref="Guid"/> object identifying a related activity.
        /// </param>
        public override void TraceTransfer(
            TraceEventCache eventCache, 
            string source, 
            int id, 
            string message, 
            Guid relatedActivityId)
        {
            using (GetColorContext(TraceEventType.Transfer))
            {
                base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a <see cref="ColorContext"/> for the specified <see cref="TraceEventType"/>.
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <returns>
        /// A <see cref="ColorContext"/> for the specified <see cref="TraceEventType"/>.
        /// </returns>
        private static ColorContext GetColorContext(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Verbose:
                    return new ColorContext(ConsoleColor.DarkGray);
                case TraceEventType.Information:
                    return new ColorContext(ConsoleColor.Gray);
                case TraceEventType.Critical:
                    return new ColorContext(ConsoleColor.DarkRed);
                case TraceEventType.Error:
                    return new ColorContext(ConsoleColor.Red);
                case TraceEventType.Warning:
                    return new ColorContext(ConsoleColor.Yellow);
                case TraceEventType.Start:
                    return new ColorContext(ConsoleColor.DarkGreen);
                case TraceEventType.Stop:
                    return new ColorContext(ConsoleColor.DarkMagenta);
                case TraceEventType.Transfer:
                    return new ColorContext(ConsoleColor.DarkYellow);
                default:
                    return new ColorContext();
            }
        }

        #endregion
    }
}