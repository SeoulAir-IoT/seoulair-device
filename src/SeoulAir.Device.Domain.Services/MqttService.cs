using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SeoulAir.Device.Domain.Services
{
    public sealed class MqttService<TDto> : IMqttService<TDto>
        where TDto : class
    {
        private readonly MqttSettings _settings;
        private IMqttClient _mqttClient;

        public MqttService(AppSettings settings)
        {
            _settings = settings.MqttSettings;
        }

        public async Task CloseConnection()
        {
            if (!_mqttClient.IsConnected)
                return;

            await _mqttClient.DisconnectAsync();
            _mqttClient.Dispose();
            _mqttClient = null;
        }

        public void Dispose()
        {
            if(_mqttClient != null)
            {
                if (_mqttClient.IsConnected)
                    _mqttClient.DisconnectAsync();
                _mqttClient.Dispose();
            }
        }

        public async Task OpenConnection()
        {
            MqttFactory factory = new MqttFactory();
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(_settings.BrokerAddress, _settings.BrokerPort)
                .WithClientId("deviceClient")
                .Build();

            _mqttClient = factory.CreateMqttClient();
            await _mqttClient.ConnectAsync(options);
        }

        public async Task SendDto(TDto messageObject)
        {
            string jsonObject = JsonConvert.SerializeObject(messageObject);

            MqttApplicationMessage Message = new MqttApplicationMessageBuilder()
                .WithTopic(_settings.PublishTopic)
                .WithPayload(jsonObject)
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(Message, CancellationToken.None);
        }
    }
}
