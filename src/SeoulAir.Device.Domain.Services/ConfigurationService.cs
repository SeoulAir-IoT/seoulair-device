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
            _appSettings.DeviceSettings.SendingTreshold = newTreshold;
        }

        public void Update(string newDeviceType)
        {
            _appSettings.DeviceSettings.Type = newDeviceType;
        }

        public void Update(DeviceSettings newSettings)
        {
            _appSettings.DeviceSettings.Type = newSettings.Type;
            _appSettings.DeviceSettings.SendingTreshold = newSettings.SendingTreshold;
        }
    }
}
