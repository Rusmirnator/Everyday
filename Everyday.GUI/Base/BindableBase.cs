using Everyday.Core.Attributes;
using Everyday.Core.Dictionaries;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Everyday.GUI.Base
{
    /// <summary>
    /// Contains helpful features simplifing Binding.
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        #region Fields & Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private ConcurrentDictionary<string, object> DynamicStorage { get; }
        private ConcurrentDictionary<string, ICommand> BindableCommands { get; }
        #endregion

        #region CTOR
        protected BindableBase()
        {
            DynamicStorage = new();
            BindableCommands = new();
            ConvertCommands();
        }
        #endregion

        #region Public API

        #region PropertyChanged
        /// <summary>
        /// Manually raises PropertyChanged event for provided member.
        /// </summary>
        /// <param name="name"></param>
        protected void RaisePropertyChanged(string name)
        {
            OnPropertyChanged(name);
        }
        #endregion

        #region Setters & Getters
        /// <summary>
        /// Manual get method - utilizes backing field provided as a parameter. Use in case performance is at high priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <returns></returns>
        protected static T GetValue<T>(ref T storage)
        {
            return storage;
        }

        /// <summary>
        /// Automatic get method - does not need external backing field to be provided. Use in case convenience is at high priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return DynamicStorage.TryGetValue(propertyName, out object storage) ? (T)storage : default;
        }

        /// <summary>
        /// Manual set method - utilizes backing field provided as a parameter, raises PropertyChanged event. 
        /// Use in case performance is at high priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <returns></returns>
        protected bool SetValue<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Automatic set method - does not need external backing field to be provided, raises PropertyChanged event. 
        /// Use in case convenience is at high priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (DynamicStorage.TryAdd(propertyName, value))
            {
                OnPropertyChanged(propertyName);

                return true;
            }

            if (DynamicStorage.TryGetValue(propertyName, out object storage)
                    && DynamicStorage.TryUpdate(propertyName, value, storage))
            {
                OnPropertyChanged(propertyName);

                return true;
            }
            return false;
        }
        #endregion

        #endregion

        #region EventHandlers
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private API
        private void ConvertCommands()
        {
            foreach (MethodInfo method in GetMethods())
            {
                _ = BindableCommands.TryAdd(method.Name, CreateBindableCommand(method));
            }
        }

        private ICommand CreateBindableCommand(MethodInfo method)
        {
            var attribute = MethodBase
                                .GetMethodFromHandle(method.MethodHandle)
                                    .GetCustomAttributes(true)
                                        .Cast<Attribute>()
                                            .FirstOrDefault(attr => attr is CommandAttribute or AsyncCommandAttribute);

            return RecognizeCommandType(method, out ParameterInfo parameter) switch
            {
                CommandType.Synchronous => new BindableCommand(
                                        () => method.Invoke(null, null),
                                        () => (bool)GetCanExecute(attribute).Invoke(null, null)),

                CommandType.SynchronousParametrized => new BindableCommand<object>(
                                        (parameter) => method.Invoke(null, new object[] { parameter }),
                                        (parameter) => (bool)GetCanExecute(attribute).Invoke(null, new object[] { parameter })),

                CommandType.Asynchronous => new BindableAsyncCommand(async
                                        () => await (Task)method.Invoke(null, null),
                                        () => (bool)GetCanExecute(attribute).Invoke(null, null)),

                CommandType.AsynchronousParametrized => new BindableAsyncCommand<object>(async
                                        (parameter) => await (Task)method.Invoke(null, new object[] { parameter }),
                                        (parameter) => (bool)GetCanExecute(attribute).Invoke(null, new object[] { parameter })),
                _ => null,
            };
        }

        private IEnumerable<MethodInfo> GetMethods()
        {
            var methods = GetType()
                            .GetMethods()
                                .Where(m => MethodBase
                                    .GetMethodFromHandle(m.MethodHandle)
                                        .GetCustomAttributes(true)
                                            .Where(att => att is CommandAttribute or AsyncCommandAttribute)
                                                .Any());
            return methods;
        }

        private static CommandType RecognizeCommandType(MethodInfo method, out ParameterInfo parameter)
        {
            parameter = null;
            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Any())
            {
                parameter = parameters.FirstOrDefault();
            }

            switch (method.ReturnType == typeof(Task))
            {
                case true:
                    return parameter is not null ? CommandType.AsynchronousParametrized : CommandType.Asynchronous;
                case false:
                    return parameter is not null ? CommandType.SynchronousParametrized : CommandType.Synchronous;
            }
        }

        private MethodInfo GetCanExecute(Attribute attribute)
        {
            return GetType().GetMethod((attribute as CommandAttribute).CanExecuteMethodName);
        }
        #endregion
    }
}
