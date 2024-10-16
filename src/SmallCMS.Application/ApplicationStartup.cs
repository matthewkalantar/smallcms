using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SmallCMS.Application
{
    public static class ApplicationStartup
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
