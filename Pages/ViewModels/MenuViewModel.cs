using Everyday.GUI.Base;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Fields & Properties
        public ICommand NavigateCommand { get; set; }
        #endregion

        #region CTOR
        public MenuViewModel()
        {
            NavigateCommand = new Command(async (selectedModule) => await NavigateAsync(selectedModule));
        }
        #endregion

        #region Commands
        private static async Task NavigateAsync(object selectedModule)
        {
            await GoToPageAsync(selectedModule as string);
        }
        #endregion
    }
}
