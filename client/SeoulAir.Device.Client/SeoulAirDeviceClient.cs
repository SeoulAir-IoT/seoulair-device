using Refit;

namespace SeoulAir.Device.Client;

public class SeoulAirDeviceClient : ISeoulAirDeviceClient
{
    public IRowsApi RowsApi { get; }
    public IAirQualitySensorApi AirQualitySensorApi { get; }
    public ISignalLightApi SignalLightApi { get; }

    public SeoulAirDeviceClient(SeoulAirDeviceSettings settings)
    {
        RowsApi = RestService.For<IRowsApi>(settings.ServiceUrl);
        AirQualitySensorApi = RestService.For<IAirQualitySensorApi>(settings.ServiceUrl);
        SignalLightApi = RestService.For<ISignalLightApi>(settings.ServiceUrl);
    }
}
