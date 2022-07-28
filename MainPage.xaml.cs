namespace Everyday.GUI;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }

	private void ContentPage_Loaded(object sender, EventArgs e)
	{
		(BindingContext as MainPageViewModel).InitCommand.Execute(null);
	}
}

