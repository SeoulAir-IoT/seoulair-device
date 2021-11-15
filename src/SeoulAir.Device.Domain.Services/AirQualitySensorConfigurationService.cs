using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;
using SeoulAir.Device.Domain.Resources;
using System;

namespace SeoulAir.Device.Domain.Services;

public class AirQualitySensorConfigurationService : IDeviceConfigurationService
{
    private readonly IOptions<SeoulAirDeviceOptions> _options;

    public AirQualitySensorConfigurationService(IOptions<SeoulAirDeviceOptions> options)
    {
        _options = options;
    }

    public SeoulAirDeviceOptions ActiveConfiguration => _options.Value;

    public void UpdateSensorName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(string.Format(Strings.ParameterNullOrEmptyMessage, nameof(name)));

        _options.Value.Name = name;
    }

    public void UpdateReadingDelay(ushort delay)
    {
        _options.Value.ReadingDelayMs = delay.ToString();
    }
}
