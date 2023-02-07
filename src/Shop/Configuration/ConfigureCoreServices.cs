using Shop.ApplicationCore.Interfaces;
using Shop.Infrastructure;

namespace Shop.Configuration
{
    public static class ConfigureCoreServices
    {
        internal static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
