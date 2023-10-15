using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence.Repository;

namespace ShoesShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ServerConnection");

            services.AddDbContext<ShopDbContext>(option => option.UseSqlServer(connectionString));
            services.AddScoped<IShoesRepository, ShoesRepository>();
            services.AddScoped<IDescriptionRepository, DescriptionRepository>();
            services.AddScoped<IShoesSizeRepository, ShoesSizeRepository>();

            return services;
        }
    }
}
