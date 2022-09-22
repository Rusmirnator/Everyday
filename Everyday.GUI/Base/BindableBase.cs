using Everyday.Core.Attributes;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq.Expressions;
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
        private ConcurrentDictionary<string, ICommand> CommandStorage { get; }
        #endregion

        #region CTOR
        protected BindableBase()
        {
            DynamicStorage = new();
            CommandStorage = new();
            CreateCommands();
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
        private void CreateCommands()
        {
            //var commandParameters = GetType()
                                        //.GetRuntimeMethods()
                                        //    .FirstOrDefault(m => m.Name == "RefreshAsync")
                                        //        .GetParameters()
                                        //            .Select(p => Expression.Parameter(p.ParameterType))
                                        //                .ToArray();

            //handler = Expression.Lambda
            //    (
            //            eventInfo.EventHandlerType,
            //            Expression.Call(Expression.Constant(action), actionInvoke, commandParameters[0], commandParameters[1]),
            //            commandParameters
            //    ).Compile();

            //eventInfo.AddEventHandler(item, handler);
        }
        #endregion
    }
}
