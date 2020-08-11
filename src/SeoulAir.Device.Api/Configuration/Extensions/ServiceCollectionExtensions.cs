using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Device.Api.HelperClasses;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;

namespace SeoulAir.Device.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services,IConfiguration configuration)
        {
            ISettingsReader reader = new SettingsReader(configuration);

            AppSettings settings = reader.ReadAllSettings();
            services.AddSingleton(settings);

            return services;
        }

    }
}
