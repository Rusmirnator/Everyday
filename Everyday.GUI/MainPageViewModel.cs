using Everyday.GUI.Base;
using System.Windows.Input;
using Everyday.GUI.Utilities;
using Everyday.Domain.Attributes;

namespace Everyday.GUI
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields & Properties
        public bool IsWaitIndicatorVisible
        {
            get { return GetValue<bool>(); }
            set { _ = SetValue(value); }
        }

        public string Login
        {
            get { return GetValue<string>(); }
            set
            {
                if (SetValue(value))
                {
                    (LoginCommand as BindableAsyncCommand)
                        .RaiseCanExecuteChanged();
                }
            }
        }
        public string Password
        {
            get { return GetValue<string>(); }
            set
            {
                if (SetValue(value))
                {
                    (LoginCommand as BindableAsyncCommand)
                        .RaiseCanExecuteChanged();
                }
            }
        }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region CTOR
        public MainPageViewModel()
        {
            LoginCommand = new BindableAsyncCommand(async
                () => await LoginAsync(),
                () => CanLogin(),
                (exception) => ThrowException(exception));
        }
        #endregion

        #region Commands
        [AsyncCommand]
        public async Task LoginAsync()
        {
            IsWaitIndicatorVisible = true;

            //IConveyOperationResult res = await authorizationService
            //                                        .LoginAsync(Login, Password)
            //                                            .ConfigureAwait(true);

            IsWaitIndicatorVisible = false;

            //if (res.StatusCode != 0)
            //{
            //    await AnnounceAsync("Error", res.Message, "Ok");

            //    return;
            //}

            await Shell.Current.GoToAsync("Menu");
        }
        #endregion

        #region CanExecute
        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }
        #endregion

        private static void ThrowException(Exception ex)
        {
            AnnounceAsync("Error", ex.Message, "Ok")
                .FireAndForget();
        }
    }
}
