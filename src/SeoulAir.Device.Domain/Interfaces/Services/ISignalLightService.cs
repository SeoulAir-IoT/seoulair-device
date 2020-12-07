using SeoulAir.Device.Domain.Enums;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface ISignalLightService
    {
        bool IsOn(string stationCode);
        LightColor GetActiveColor(string stationCode);
        void TurnOn(string stationCode);
        void TurnOff(string stationCode);
        void ChangeColor(string stationCode, LightColor color);
    }
}