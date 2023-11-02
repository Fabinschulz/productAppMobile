using Microsoft.Extensions.Logging;
using ProductAppMAUI.Infra.Context;
using ProductAppMAUI.ViewModel;
using ProductAppMAUI.Views;

namespace ProductAppMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var dbContext = new DataContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddTransient<ProductPage>();
            builder.Services.AddTransient<ProductViewModel>();

            builder.Services.AddTransient<ProductList>();
            builder.Services.AddTransient<ProductListViewModel>();

            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}