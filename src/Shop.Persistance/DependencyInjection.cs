using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Entities;
using ShoesShop.Persistence.Repository;

namespace ShoesShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TestConnection");

            services.AddDbContext<ShopDbContext>(option => option.UseSqlServer(connectionString));
            services.AddUnitOfWork();
            services.AddRepositories();
            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IRepositoryOf<Address>, AddressRepository>()
                           .AddScoped<IRepositoryOf<ShopCartItem>, ShopCartItemRepository>()
                           .AddScoped<IRepositoryOf<FavoritesItem>, FavoritesItemRepository>()
//                           .AddScoped<IRepositoryOf<FavoritesList>, FavoritesListRepository>()
                           .AddScoped<IRepositoryOf<Image>, ImageRepository>()
                           .AddScoped<IRepositoryOf<Model>, ModelRepository>()
                           .AddScoped<IRepositoryOf<ModelSize>, ModelSizeRepository>()
                           .AddScoped<IRepositoryOf<ModelVariant>, ModelVariantRepository>()
                           .AddScoped<IRepositoryOf<Order>, OrderRepository>()
                           .AddScoped<IRepositoryOf<OrderItem>, OrderItemRepository>()
                           .AddScoped<IRepositoryOf<Review>, ReviewRepository>()
                           .AddScoped<IRepositoryOf<Sale>, SaleRepository>()
//                           .AddScoped<IRepositoryOf<ShopCart>, ShopCartRepository>()
                           .AddScoped<IRepositoryOf<User>, UserRepository>();
        }
    }
}
