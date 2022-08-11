using Everyday.GUI.Base;
using Everyday.Services.Interfaces;
using System.Windows.Input;

namespace Everyday.GUI
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields & Properties
        private string login;
        private string password;
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpClientService httpClientService;

        public string Login
        {
            get { return GetValue(ref login); }
            set
            {
                if (SetValue(ref login, value))
                {
                    (LoginCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string Password
        {
            get { return GetValue(ref password); }
            set
            {
                if (SetValue(ref password, value))
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
            HttpResponseMessage response =
                await httpClientService
                        .CreateUnauthorized($"Home/login?login={Login}&password={Password}")
                            .PostCallAsync();

            if (response?.IsSuccessStatusCode is not true)
            {
                await AnnounceAsync("Error", response?.ReasonPhrase, "Ok");
                return;
            }
            await authorizationService.AcquireCredentialsAsync(response);

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
