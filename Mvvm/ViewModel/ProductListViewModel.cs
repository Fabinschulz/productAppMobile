
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using ProductAppMAUI.Domain.DTOs;
using ProductAppMAUI.Domain.Entities;
using ProductAppMAUI.Infra.Context;
using ProductAppMAUI.Utils;
using ProductAppMAUI.Views;
using System.Collections.ObjectModel;

namespace ProductAppMAUI.ViewModel
{
    public partial class ProductListViewModel : ObservableObject
    {
        private readonly DataContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<ProductDTO> productList = new ObservableCollection<ProductDTO>();

        public ProductListViewModel(DataContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await GetAll()));

            WeakReferenceMessenger.Default.Register<ProductMessenger>(this, (r, m) =>
            {
                ProductMessenger(m.Value);
            });

        }

        public async Task GetAll()
        {
            var lista = await _dbContext.Products.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ProductList.Add(new ProductDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                    });
                }
            }
        }

        private void ProductMessenger(ProductMessage productMessage)
        {
            var productDto = productMessage.ProductDto;

            if (productMessage.IsCreate)
            {
                ProductList.Add(productDto);
            }
            else
            {
                var product = ProductList.First(e => e.Id == productDto.Id);
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;

            }

        }

        [RelayCommand]
        private async Task AddProduct()
        {
            var productId = 0;
            var uri = $"{nameof(ProductPage)}?id={productId}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Edit(ProductDTO productDto)
        {
            var uri = $"{nameof(ProductPage)}?id={productDto.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Delete(ProductDTO productDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Excluir", "Deseja excluir o produto?", "Sim", "Não");

            if (answer)
            {
                var product = await _dbContext.Products
                    .FirstAsync(e => e.Id == productDto.Id);

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                ProductList.Remove(productDto);

            }

        }
    }
}
