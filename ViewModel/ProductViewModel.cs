
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using ProductAppMAUI.Domain.DTOs;
using ProductAppMAUI.Domain.Entities;
using ProductAppMAUI.Infra.Context;
using ProductAppMAUI.Utils;

namespace ProductAppMAUI.ViewModel
{
    public partial class ProductViewModel : ObservableObject, IQueryAttributable
    {
        private readonly DataContext _dbContext;

        [ObservableProperty]
        private ProductDTO productDto = new ProductDTO();

        [ObservableProperty]
        private string title;

        private int ProductId;

        [ObservableProperty]
        private bool isLoading = false;

        public ProductViewModel(DataContext context)
        {
            _dbContext = context;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            ProductId = id;
            if (ProductId == 0)
            {
                Title = "Novo Produto";
            }
            else
            {
                Title = "Editar Produto";
                IsLoading = true;
                await Task.Run(async () =>
                {
                    var product = await _dbContext.Products.FirstAsync(e => e.Id == ProductId);
                    ProductDto.Id = product.Id;
                    ProductDto.Name = product.Name;
                    ProductDto.Description = product.Description;
                    ProductDto.Price = product.Price;

                    MainThread.BeginInvokeOnMainThread(() => { IsLoading = false; });
                });
            }

        }

        [RelayCommand]
        private async Task Create()
        {
            IsLoading = true;
            ProductMessage message = new ProductMessage();

            await Task.Run(async () =>
            {
                if (ProductId == 0)
                {
                    var tbProduct = new Product
                    {
                        Name = ProductDto.Name,
                        Description = ProductDto.Description,
                        Price = ProductDto.Price,
                    };

                    _dbContext.Products.Add(tbProduct);
                    await _dbContext.SaveChangesAsync();

                    ProductDto.Id = tbProduct.Id;
                    message = new ProductMessage()
                    {
                        IsCreate = true,
                        ProductDto = ProductDto
                    };

                }
                else
                {
                    var product = await _dbContext.Products.FirstAsync(e => e.Id == ProductId);
                    product.Name = ProductDto.Name;
                    product.Description = ProductDto.Description;
                    product.Price = ProductDto.Price;

                    await _dbContext.SaveChangesAsync();

                    message = new ProductMessage()
                    {
                        IsCreate = false,
                        ProductDto = ProductDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    WeakReferenceMessenger.Default.Send(new ProductMessenger(message));
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }

    }
}
