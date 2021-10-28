namespace SeoulAir.Device.Client
{
    public interface ISeoulAirDeviceClient
    {
        IRowsApi RowsApi { get; }
        IAirQualitySensorApi AirQualitySensorApi { get; }
        ISignalLightApi SignalLightApi { get; }
    }
}