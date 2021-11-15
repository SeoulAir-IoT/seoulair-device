using SeoulAir.Device.Domain.Options;

namespace SeoulAir.Device.Domain.Interfaces.Services;

public interface IDeviceConfigurationService
{
    SeoulAirDeviceOptions ActiveConfiguration { get; }
    void UpdateSensorName(string name);
    void UpdateReadingDelay(ushort delay);
}
