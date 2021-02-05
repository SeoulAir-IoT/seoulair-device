using System;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;
using SeoulAir.Device.Domain.Resources;

namespace SeoulAir.Device.Domain.Services
{
    public class AirQualitySensorConfigurationService : IAirQualitySensorConfigurationService
    {
        private readonly IOptions<AirQualitySensorOptions> _options;

        public AirQualitySensorConfigurationService(IOptions<AirQualitySensorOptions> options)
        {
            _options = options;
        }

        public AirQualitySensorOptions ActiveConfiguration => _options.Value;
        
        public void UpdateSensorName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(string.Format(Strings.ParameterNullOrEmptyMessage, nameof(name)));
            
            _options.Value.Name = name;
        }

        public void UpdateReadingDelay(ushort delay)
        {
            _options.Value.ReadingDelayMs = delay;
        }
    }
}