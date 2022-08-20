using System.Windows.Input;

namespace Everyday.GUI.Base
{
    public class BaseViewModel : ParameterNetwork
    {
        #region Fields & Properties
        public ICommand InitCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        #endregion

        protected BaseViewModel() : base()
        {

        }

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
            try
            {
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                {
                    await Shell.Current.GoToAsync("Error");
                }
            }
        }
        #endregion
    }
}
