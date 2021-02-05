namespace SeoulAir.Device.Domain.Dtos
{
    public class AppSettings
    {
        public MqttSettings MqttSettings { get; set; }
        public DeviceSettings DeviceSettings { get; set; }
    }
}
