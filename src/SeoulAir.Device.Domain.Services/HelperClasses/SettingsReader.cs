using Microsoft.Extensions.Configuration;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Exceptions;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.HelperClasses
{
    public class SettingsReader : ISettingsReader
    {
        private readonly IConfiguration _appConfiguration;

        public SettingsReader(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public AppSettings ReadAllSettings()
        {
            AppSettings appSettings = new AppSettings();

            ReadMqttSettings(appSettings);
            ReadDeviceSettings(appSettings);

            return appSettings;
        }

        public bool TryReadSetting(string name, out string value)
        {
            value = _appConfiguration[name];

            return !string.IsNullOrWhiteSpace(value);
        }

        private void ReadMqttSettings(AppSettings appSettings)
        {
            appSettings.MqttSettings = new MqttSettings();

            if (!TryReadSetting("Mqtt:Broker.Address", out var tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Address"));

            appSettings.MqttSettings.BrokerAddress = tempHolder;

            if (!TryReadSetting("Mqtt:Broker.Port", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Port"));

            appSettings.MqttSettings.BrokerPort = short.Parse(tempHolder);
        }

        private void ReadDeviceSettings(AppSettings appSettings)
        {
            appSettings.DeviceSettings = new DeviceSettings();

            if (!TryReadSetting("Device:Type", out var tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Device:Type"));

            appSettings.DeviceSettings.Type = tempHolder;

            if (!TryReadSetting("Device:Treshold", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Device:Treshold"));

            appSettings.DeviceSettings.SendingTreshold = uint.Parse(tempHolder);

            if (!TryReadSetting("Device:Data.Path", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Device:Data.Path"));

            appSettings.DeviceSettings.DataPath = tempHolder;
        }
    }
}
