using Everyday.Core.Attributes;
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
            NavigateCommand = new BindableAsyncCommand<object>(async (selectedModule) => await NavigateAsync(selectedModule));
        }
        #endregion

        #region Commands
        [AsyncCommand]
        public static async Task NavigateAsync(object selectedModule)
        {
            await GoToPageAsync(selectedModule as string);
        }
        #endregion
    }
}
