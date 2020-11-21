using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Options;

namespace SeoulAir.Device.Domain.Services
{
    public sealed class MqttService<TDto> : IMqttService<TDto>
        where TDto : class
    {
        private readonly MqttConnectionOptions _settings;
        private IMqttClient _mqttClient;

        public MqttService(IOptions<MqttConnectionOptions> mqttConnectionSettings)
        {
            _settings = mqttConnectionSettings.Value;
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
                .WithClientId(_settings.SenderClientId)
                .Build();

            _mqttClient = factory.CreateMqttClient();
            await _mqttClient.ConnectAsync(options);
        }

        public async Task SendDto(TDto messageObject)
        {
            string jsonObject = JsonConvert.SerializeObject(messageObject);

            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(_settings.Topic)
                .WithPayload(jsonObject)
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
        }
    }
}
