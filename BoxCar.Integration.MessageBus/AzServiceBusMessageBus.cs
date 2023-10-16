using BoxCar.Integration.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System.Text;

namespace BoxCar.Integration.MessageBus
{
    public class AzServiceBusMessageBus : IMessageBus
    {
        private string connectionString =
            "Endpoint=sb://<your-namespace>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<your_key>";

        public async Task PublishMessage(IntegrationBaseMessage message, string topicName)
        {
            ISenderClient topicClient = new TopicClient(connectionString, topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var serviceBusMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await topicClient.SendAsync(serviceBusMessage);
            Console.WriteLine($"Sent message to {topicClient.Path}");
            await topicClient.CloseAsync();

        }
    }
}
