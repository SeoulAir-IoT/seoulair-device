using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;

namespace SeoulAir.Device.Domain.Services.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IMqttService<RawDataInstanceDto>, MqttService<RawDataInstanceDto>>();
        services.AddSingleton<ISignalLightService, SignalLightService>();

        return services;
    }
}
