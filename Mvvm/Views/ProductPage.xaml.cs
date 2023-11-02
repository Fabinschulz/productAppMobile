using ProductAppMAUI.ViewModel;

namespace ProductAppMAUI.Views;

public partial class ProductPage : ContentPage
{
    public ProductPage(ProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}