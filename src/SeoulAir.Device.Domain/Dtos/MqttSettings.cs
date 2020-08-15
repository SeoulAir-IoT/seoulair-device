namespace SeoulAir.Device.Domain.Dtos
{
    public class MqttSettings
    {
        public string BrokerAddress { get; set; }
        public short BrokerPort { get; set; }
        public string PublishTopic { get; set; }
    }
}
