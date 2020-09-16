using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface IConfigurationService
    {
        DeviceSettings GetCurrentParameters();

        void Update(uint newTreshold);

        void Update(string newDeviceType);

        void Update(DeviceSettings newConfiguration);
    }
}
