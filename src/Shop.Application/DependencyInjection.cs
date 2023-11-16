using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Requests.Queries.OutputVMs.Profiles;

namespace ShoesShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new VmProfiles());
            });
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}
