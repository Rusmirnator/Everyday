using Everyday.GUI.Base;
using Everyday.Data.Interfaces;
using System.Windows.Input;
using Everyday.Services.Interfaces;
using Everyday.Core.Interfaces;

namespace Everyday.GUI
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields & Properties
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpClientService httpClientService;

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
        public MainPageViewModel(IAuthorizationService authorizationService, IHttpClientService httpClientService)
        {
            LoginCommand = new Command(async () => await LoginAsync(), () => CanLogin());
            this.authorizationService = authorizationService;
            this.httpClientService = httpClientService;
        }

        #endregion

        #region Commands
        private async Task LoginAsync()
        {
            IConveyOperationResult res = await authorizationService.AcquireCredentialsAsync(Login, Password);

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
    }
}
