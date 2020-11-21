using SeoulAir.Device.Domain.Enums;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface ISignalLightService
    {
        bool IsOn { get; }
        LightColor ActiveColor { get; }
        void TurnOn();
        void TurnOff();
        void ChangeColor(LightColor color);
    }
}