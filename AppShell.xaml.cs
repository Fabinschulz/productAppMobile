using ProductAppMAUI.Views;

namespace ProductAppMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
        }
    }
}