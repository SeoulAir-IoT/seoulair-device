namespace SeoulAir.Device.Domain.Options
{
    public class MqttConnectionOptions
    {
        public const string OptionsPath = "Mqtt";

        public string BrokerAddress { get; set; }
        public int BrokerPort { get; set; }
        public string Topic { get; set; }
        public string SenderClientId { get; set; }
    }
}
