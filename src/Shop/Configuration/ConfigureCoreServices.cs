using Shop.ApplicationCore.Interfaces;
using Shop.ApplicationCore.Services;
using Shop.Infrastructure;

namespace Shop.Configuration
{
    public static class ConfigureCoreServices
    {
        internal static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IBasketService, Basketservice>();

            return services;
        }
    }
}
