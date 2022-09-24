using Everyday.GUI.Base;
using Everyday.Data.Interfaces;
using System.Windows.Input;
using Everyday.Services.Interfaces;
using Everyday.Core.Interfaces;
using Everyday.Core.Attributes;

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
                    (LoginCommand as Command).ChangeCanExecute();
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
                    (LoginCommand as Command).ChangeCanExecute();
                }
            }
        }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region CTOR
        public MainPageViewModel(IAuthorizationService authorizationService)
        {
            LoginCommand = new Command(async () => await LoginAsync(), () => CanLogin());
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

        [Command]
        public void Beep()
        {
            System.Diagnostics.Debug.WriteLine("beep!");
        }
    }
}
