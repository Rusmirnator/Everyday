using Everyday.GUI.Pages.ViewModels;
using ZXing.Net.Maui;

namespace Everyday.GUI.Pages.Views;

public partial class Scanner : ContentPage
{
    public Scanner()
    {
        InitializeComponent();
        BarcodeReader.Options = new()
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true
        };
    }

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (BarcodeResult barcode in e.Results)
        {
            Console.WriteLine($"Barcodes: format: {barcode.Format}, value: {barcode.Value}");
        }
    }

    private void OnBarcodeDetected(object sender, EventArgs e)
    {

    }
}