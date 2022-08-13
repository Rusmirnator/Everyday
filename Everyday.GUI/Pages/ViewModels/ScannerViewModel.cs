using Everyday.GUI.Base;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        private readonly ILogger<ScannerViewModel> logger;

        public ICommand GetItemByBarcodeCommand { get; set; }
        public ICommand ExecuteCommand { get; set; }

        public ScannerViewModel(ILogger<ScannerViewModel> logger)
        {
            GetItemByBarcodeCommand = new Command(() => GetItemByBarcodeAsync());
            ExecuteCommand = new Command(() => Execute());
            this.logger = logger;
        }
        public async void GetItemByBarcodeAsync()
        {
            logger.LogTrace("ScannerViewModel.GetItemByBarcodeAsync()");
            await Task.CompletedTask;
        }

        private void Execute()
        {
            logger.LogTrace("ScannerViewModel.Execute()");
        }
    }
}
