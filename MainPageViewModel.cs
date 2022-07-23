using Everyday.GUI.Base;
using Everyday.Services.Interfaces;
using Everyday.Services.Services;
using System.Windows.Input;

namespace Everyday.GUI
{
    public class MainPageViewModel : BaseViewModel
    {
        private string login;
        private string password;
        private readonly IHttpClientService http;

        public string Login { get { return GetValue<string>(); } set { _ = SetValue(ref login, value); } }
        public string Password { get { return GetValue<string>(); } set { _ = SetValue(ref password, value); } }
        public ICommand LoginCommand { get; set; }

        public MainPageViewModel(IHttpClientService http)
        {
            LoginCommand = new Command(async () => await LoginAsync());
            this.http = http;
        }

        public MainPageViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            http = new HttpClientService("https://localhost:44318/");
        }
        private async Task LoginAsync()
        {
            var token = await http.Create("api/Home/login?login=admin&password=testPassword").PostCallAsync();
        }
    }
}
