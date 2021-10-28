namespace SeoulAir.Device.Domain.Options
{
    public class AirQualitySensorOptions
    {
        public static string AppSettingsPath => "AirQualitySensor";

        public string Name { get; set; }
        public ushort ReadingDelayMs { get; set; }
        public string DataPath { get; set; }
    }
}
