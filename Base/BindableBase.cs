using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Everyday.GUI.Base
{
    public class BindableBase : INotifyPropertyChanged
    {
        #region Fields & Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private ConcurrentDictionary<string, object> DynamicStorage { get; }
        #endregion

        #region CTOR
        public BindableBase()
        {
            DynamicStorage = new();
        }
        #endregion


        #region Public API

        #region PropertyChanged
        protected void RaisePropertyChanged(string name)
        {
            OnPropertyChanged(name);
        }
        #endregion

        #region Setters & Getters
        protected static T GetValue<T>(ref T storage)
        {
            return storage;
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return DynamicStorage.TryGetValue(propertyName, out object storage) ? (T)storage : default;
        }

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
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
