using ProductAppMAUI.ViewModel;

namespace ProductAppMAUI.Views;

public partial class ProductList : ContentPage
{
	public ProductList(ProductListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}