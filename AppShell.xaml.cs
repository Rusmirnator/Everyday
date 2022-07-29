using Everyday.GUI.Pages.Views;

namespace Everyday.GUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        InitializeRoutes();
    }

    private static void InitializeRoutes()
    {
        Routing.RegisterRoute(nameof(Menu), typeof(Menu));
    }
}
