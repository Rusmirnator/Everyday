using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Everyday.GUI.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields & Properties
        public ICommand InitCommand { get; set; }
        protected BaseViewModel ParentViewModel { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Diagnostics.Debug.WriteLine($"Property {propertyName} changed!");
        }
        #endregion

        #region Public API

        #region Setters & Getters
        protected static T GetValue<T>(ref T storage) where T : class
        {
            return storage;
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
        #endregion

        #region Dialogs
        protected static async Task AnnounceAsync(string title, string message, string acceptText, string cancelText = null)
        {
            if (!string.IsNullOrEmpty(cancelText))
            {
                await Application.Current.MainPage.DisplayAlert(title, message, acceptText, cancelText);
            }
            await Application.Current.MainPage.DisplayAlert(title, message, acceptText);
        }

        protected static async Task<string> DecideAsync(string title, string cancelText, params string[] args)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancelText, null, args);
        }
        #endregion

        #region Navigation
        protected static async Task GoToPageAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }
        #endregion

        #endregion
    }
}
