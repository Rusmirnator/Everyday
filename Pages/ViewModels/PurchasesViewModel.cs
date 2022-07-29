using Everyday.GUI.Base;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class PurchasesViewModel : BaseViewModel
    {
        public ICommand OpenScannerCommand { get; set; }

        public PurchasesViewModel()
        {
            OpenScannerCommand = new Command(async () => await OpenScannerAsync());
        }

        private static async Task OpenScannerAsync()
        {
            await GoToPageAsync("Scanner");
        }
    }
}
