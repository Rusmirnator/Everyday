using Everyday.Services.Interfaces;

namespace Everyday.GUI;

public partial class MainPage : ContentPage
{
    private readonly ICryptographyService cryptoService;
    int count = 0;

    public MainPage(ICryptographyService cryptoService)
	{
		InitializeComponent();
        this.cryptoService = cryptoService;
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(cryptoService.Decrypt("X7Pvr0q2QKKFbTZfgTbmyQ=="));
		SemanticScreenReader.Announce(cryptoService.Encrypt("testPassword"));
	}
}

