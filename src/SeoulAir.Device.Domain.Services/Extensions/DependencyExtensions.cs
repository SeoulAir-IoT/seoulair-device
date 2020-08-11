using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Device.Domain.Interfaces.Services;

namespace SeoulAir.Device.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationService, ConfigurationService>();
            return services;
        }
    }
}
