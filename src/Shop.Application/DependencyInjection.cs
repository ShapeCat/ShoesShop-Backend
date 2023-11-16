using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Requests.Adresses.OutputVMs;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.Prices.OutputVMs;

namespace ShoesShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMappingProfiles();
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            return services.AddAutoMapper(cfg => cfg.AddProfiles(new Profile[]
            {

            }));
        }
    }
}
