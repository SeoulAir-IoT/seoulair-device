using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Domain.Interfaces.HelperClasses
{
    public interface ISettingsReader
    {
        AppSettings ReadAllSettings();
        bool TryReadSetting(string name, out string value);

    }
}
