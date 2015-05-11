// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyResolver.cs" company="Microsoft Corporation">
//   Copyright 2015 Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Microsoft.MessageBridge.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    /// <summary>
    ///     Unity configuration.
    /// </summary>
    public static class DependencyResolver
    {
        #region Static Fields

        /// <summary>
        ///     The Unity container.
        /// </summary>
        private static readonly IUnityContainer Container = LoadContainer();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Resolves an instance of the requested type from the container.
        /// </summary>
        /// <typeparam name="T">
        ///     The type to resolve.
        /// </typeparam>
        /// <returns>
        ///     A resolved instance of the requested type.
        /// </returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Resolves an instance of the requested type with the given name from the container.
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve.
        /// </typeparam>
        /// <param name="name">
        /// The name of the object to retrieve.
        /// </param>
        /// <returns>
        /// A resolved instance of the requested type.
        /// </returns>
        public static T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        /// <summary>
        /// Resolves an instance of the requested type with the given name from the container.
        /// </summary>
        /// <param name="type">
        /// The type to resolve.
        /// </param>
        /// <param name="name">
        /// The name of the object to retrieve.
        /// </param>
        /// <returns>
        /// A resolved instance of the requested type.
        /// </returns>
        public static object Resolve(Type type, string name)
        {
            return Container.Resolve(type, name);
        }

        /// <summary>
        ///     Resolves all instances of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>The collection of all instances of the type.</returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Loads the Unity container.
        /// </summary>
        /// <returns>
        ///     The loaded container.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "The type of exception thrown is not known until runtime.")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "The container will last for the lifetime of the application.")]
        private static UnityContainer LoadContainer()
        {
            var unityContainer = new UnityContainer();

            try
            {
                unityContainer.LoadConfiguration();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }

            return unityContainer;
        }

        #endregion
    }
}