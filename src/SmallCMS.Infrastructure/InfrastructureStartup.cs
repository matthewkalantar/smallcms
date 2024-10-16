using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmallCMS.Infrastructure.Data;

namespace SmallCMS.Infrastructure
{
    public static class InfrastructureStartup
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SmallCmsDbContext>(options => options.UseSqlite(configuration["ConnectionStrings:Database"]));

            services.AddScoped<ISmallCmsDbContext>(provider => provider.GetRequiredService<SmallCmsDbContext>());

            return services;
        }
    }
}
