using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Common.Mapping;
using ShoesShop.Application.Common.Validation;

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
            return services.AddAutoMapper(cfg => cfg.AddProfiles(VmProfiles.AllProfiles));
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        }
    }
}
