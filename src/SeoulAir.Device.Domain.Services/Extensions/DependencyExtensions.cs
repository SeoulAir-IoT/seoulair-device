using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Services.HelperClasses;

namespace SeoulAir.Device.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<ICsvReader<RawDataInstanceDto>, CsvReader<RawDataInstanceDto>>();
            services.AddSingleton<IRowConverter<RawDataInstanceDto>, RawDataInstanceRowConverter>();
            services.AddSingleton<IMqttService<RawDataInstanceDto>, MqttService<RawDataInstanceDto>>();

            return services;
        }
    }
}
