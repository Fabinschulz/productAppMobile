using CommunityToolkit.Mvvm.ComponentModel;


namespace ProductAppMAUI.Domain.DTOs
{
    public partial class ProductDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string description;

        [ObservableProperty]
        public decimal price;
    }
}
