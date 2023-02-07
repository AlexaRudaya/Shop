using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.ApplicationCore.Identity;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<CatalogContext>(context => context.UseSqlServer(configuration.GetConnectionString("CatalogConnection")));
            services.AddDbContext<AppIdentityDbContext>(context => context.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        }
    }
}
