using Everyday.Application.Common.Interfaces.Services;
using Everyday.Infrastructure.Common.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Everyday.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpService();

            return services;
        }

        private static IServiceCollection AddHttpService(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>(serviceProvider =>
            {
                IHttpService service = serviceProvider.GetRequiredService<IHttpService>();

                service.Configuration

                return (HttpService)service;
            });

            return services;
        }
    }
}
