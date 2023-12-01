using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Common.Validation;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Authentication.OutputVMs;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.Users.OutputVMs;

namespace ShoesShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMappingProfiles();
            services.AddValidation();
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            return services.AddAutoMapper(cfg => cfg.AddProfiles(new Profile[]
            {
                new AddressVmProfiles(),
                new ImageVmProfiles(),
                new ModelVmProfiles(),
                new ModelSizeVmProfiles(),
                new ModelVariantVmProfiles(),
                new SaleVmProfiles(),
                new AuthenticationUserProfiles(),
                new UserVmProfiles(),
                new ShopCartItemsProfile(),
                new ReviewVmProfile(),
            }));
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        }
    }
}
