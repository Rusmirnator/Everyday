using Everyday.GUI.Base;
using System.Windows.Input;
using Everyday.Services.Interfaces;
using Everyday.Core.Interfaces;
using Everyday.Core.Attributes;
using Everyday.GUI.Utilities;

namespace Everyday.GUI
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields & Properties
        private readonly IAuthorizationService authorizationService;
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
        public MainPageViewModel(IAuthorizationService authorizationService)
        {
            LoginCommand = new BindableAsyncCommand(async
                () => await LoginAsync(),
                () => CanLogin(),
                (exception) => ThrowException(exception));
            this.authorizationService = authorizationService;
        }
        #endregion

        #region Commands
        private async Task LoginAsync()
        {
            IsWaitIndicatorVisible = true;

            IConveyOperationResult res = await authorizationService
                                                    .AcquireCredentialsAsync(Login, Password)
                                                        .ConfigureAwait(true);

            IsWaitIndicatorVisible = false;

            if (res.StatusCode != 0)
            {
                await AnnounceAsync("Error", res.Message, "Ok");

                return;
            }

            await Shell.Current.GoToAsync("Menu");
        }
        #endregion

        #region CanExecute
        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }
        #endregion

        public static void ThrowException(Exception ex)
        {
            AnnounceAsync("Error", ex.Message, "Ok")
                .FireAndForget();
        }
    }
}
