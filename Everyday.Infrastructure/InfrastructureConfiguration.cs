using Everyday.Application.Common.Interfaces.Services;
using Everyday.Infrastructure.Common.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Everyday.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpService(config);

            return services;
        }

        private static IServiceCollection AddHttpService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IHttpService, HttpService>(serviceProvider =>
            {
                HttpService service = new()
                {
                    Configuration = new()
                    {
                        BaseAddress = new Uri(config["Https:API_BASE_URI"]),
                        Timeout = TimeSpan.FromSeconds(double.TryParse(config["Https:Timeout"],
                                                                       out double timeout) ? timeout : 15)
                    }
                };

                return service;
            });

            return services;
        }
    }
}
