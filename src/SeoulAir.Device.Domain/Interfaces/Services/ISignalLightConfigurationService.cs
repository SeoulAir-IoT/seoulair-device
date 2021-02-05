using SeoulAir.Device.Domain.Enums;
using SeoulAir.Device.Domain.Options;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface ISignalLightConfigurationService
    {
        SignalLightOptions ActiveConfiguration { get; }
        void UpdateName(string name);
        void UpdateDefaultLight(LightColor color);
    }
}
