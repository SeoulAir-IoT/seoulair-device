using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;

namespace SeoulAir.Device.Domain.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AppSettings _appSettings;

        public ConfigurationService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public DeviceSettings GetCurrentParameters()
            => _appSettings.DeviceSettings;

        public void Update(uint newTreshold)
        {
            _appSettings.DeviceSettings.ReadingDelayMs = newTreshold;
        }

        public void Update(string newDeviceType)
        {
            _appSettings.DeviceSettings.Name = newDeviceType;
        }

        public void Update(DeviceSettings newConfiguration)
        {
            _appSettings.DeviceSettings.Name = newConfiguration.Name;
            _appSettings.DeviceSettings.ReadingDelayMs = newConfiguration.ReadingDelayMs;
        }
    }
}
