﻿using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Everyday.GUI.Base
{
    public class ParameterNetwork : BindableBase
    {
        #region Fields & Properties
        private static ConcurrentDictionary<string, Parameter> funnel;
        #endregion

        #region CTOR
        protected ParameterNetwork() : base()
        {
            funnel ??= new();
        }
        #endregion

        #region Public API
        /// <summary>
        /// Sends value as a one-time read parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        protected static bool Send<T>(string audience, string name, T value)
        {
            Parameter parameter = new(audience, name, value, typeof(T));

            return funnel.TryAdd(parameter.GetCompositeKey(), parameter);
        }

        /// <summary>
        /// Gets parameter awaiting to be received and removes it from ParameterNetwork ending its lifetime.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        protected static T Receive<T>(string name, [CallerMemberName] string audience = null)
        {
            if (funnel.TryRemove(string.Concat(audience, ".", name), out Parameter value))
            {
                return value.GetValue<T>();
            }
            return default;
        }
        #endregion
    }
}
