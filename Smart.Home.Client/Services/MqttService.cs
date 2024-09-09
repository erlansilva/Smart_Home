using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using Smart.Home.Client.Services.Interfaces;

namespace Smart.Home.Client.Services
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _client;
        private readonly string _server;

        public MqttService(IConfiguration conf)
        {
            _client = new MqttFactory().CreateMqttClient();
            _server = conf.GetValue<string>("Server");
        }

        public async Task SendMessageAsync(string topic, string msg)
        {
            if (await CheckConnectionAsync()) {
                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(msg)
                    .Build();
                
                var result = await _client.PublishAsync(applicationMessage);
            }
        }


        private async Task<bool> CheckConnectionAsync()
        {
            if (!_client.IsConnected)
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(_server).Build();               
                var response = await _client.ConnectAsync(mqttClientOptions, CancellationToken.None);
                return _client.IsConnected;
            }
            return true;
        }
    }
}
