using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Everyday.GUI.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand InitCommand { get; set; }
        protected BaseViewModel ParentViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected T GetValue<T>([CallerMemberName] string propertyName = "") where T : class
        {
            return typeof(BaseViewModel).GetProperty(propertyName)?.GetGetMethod()?.Invoke(this, null) as T;
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Diagnostics.Debug.Write($"Property {propertyName} changed!");
        }
    }
}
