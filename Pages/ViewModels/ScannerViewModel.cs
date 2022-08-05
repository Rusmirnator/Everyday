using Everyday.GUI.Base;
using Everyday.GUI.Utilities;
using System.Windows.Input;

namespace Everyday.GUI.Pages.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        public ICommand TakePhotoCommand { get; set; }
        public ICommand ExecuteCommand { get; set; }

        public ScannerViewModel()
        {
            TakePhotoCommand = new Command(() => TakePhotoAsync());
            ExecuteCommand = new Command(() => Execute());
        }
        public static async void TakePhotoAsync()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    //string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    //using Stream sourceStream = await photo.OpenReadAsync();
                    //using FileStream localFileStream = File.OpenWrite(localFilePath);

                    //await sourceStream.CopyToAsync(localFileStream);
                    await BarcodeScanner.ReadAsync(photo);
                }
            }
        }

        private static void Execute()
        {
            Console.WriteLine("Executed command form behavior!");
        }
    }
}
