// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorContext.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging.Tool
{
    using System;

    /// <summary>
    ///     Creates a color context. Changes the console to use the colors
    ///     until the context is disposed.
    ///     <example>
    ///         <code>
    /// Console.WriteLine("I am a " + Console.ForegroundColor + " message!");
    /// using(new ColorContext(ConsoleColor.Green))
    /// {
    ///   Console.WriteLine("I am a green message!");
    /// }
    /// using(new ColorContext(ConsoleColor.Red, ConsoleColor.Blue))
    /// {
    ///   Console.WriteLine("I am a red message on a blue background!");
    /// }
    /// Console.WriteLine("I am also a " + Console.ForegroundColor + " message!");
    /// </code>
    ///     </example>
    /// </summary>
    internal sealed class ColorContext : IDisposable
    {
        #region Fields

        /// <summary>
        ///     The previous background color.
        /// </summary>
        private readonly ConsoleColor previousBackgroundColor;

        /// <summary>
        ///     The previous foreground color.
        /// </summary>
        private readonly ConsoleColor previousForegroundColor;

        /// <summary>
        ///     Whether this instance is disposed.
        /// </summary>
        private bool isDisposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ColorContext" /> class.
        /// </summary>
        public ColorContext()
            : this(Console.ForegroundColor, Console.BackgroundColor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorContext"/> class using the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">
        /// The color to use for the foreground.
        /// </param>
        public ColorContext(ConsoleColor foregroundColor)
            : this(foregroundColor, Console.BackgroundColor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorContext"/> class using the specified foreground color and
        ///     background colors.
        /// </summary>
        /// <param name="foregroundColor">
        /// The color to use for the foreground
        /// </param>
        /// <param name="backgroundColor">
        /// The color to use for the background
        /// </param>
        public ColorContext(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this.isDisposed = false;
            this.previousForegroundColor = Console.ForegroundColor;
            this.previousBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Disposes the context and restores the Console to the colors
        ///     it had before the context was created.
        /// </summary>
        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            Console.ForegroundColor = this.previousForegroundColor;
            Console.BackgroundColor = this.previousBackgroundColor;
            this.isDisposed = true;
        }

        #endregion
    }
}