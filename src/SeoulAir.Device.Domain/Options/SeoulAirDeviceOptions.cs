namespace SeoulAir.Device.Domain.Options;

public class SeoulAirDeviceOptions
{
    public const string OptionsPath = "Device";

    public string Name { get; set; }
    public string StationCode { get; }
    public string ReadingDelayMs { get; set; }
    public string DataPath { get; }
}
