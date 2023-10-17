using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.Repositories;
using BoxCar.Services.WareHousing.Entities;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.Services.WareHousing.Messages;
using BoxCar.Services.WareHousing.Contracts.Messaging;

namespace BoxCar.Services.WareHousing.Messaging
{

    public class EngineAddedEventConsumer : AzServiceBusConsumerBase, IEngineAzServiceBusConsumer
    {
        private readonly string _engineAddedEventTopic;
        private readonly IReceiverClient _engineAddedMessageReceiverClient;

        public EngineAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus, ItemsRepository itemsRepository, LoggerFactory loggerFactory)
            : base(configuration, messageBus, itemsRepository, loggerFactory)
        {
            _engineAddedEventTopic = _configuration.GetValue<string>("EngineAddedEventTopic");
            _engineAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _engineAddedEventTopic, _subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _engineAddedMessageReceiverClient.RegisterMessageHandler(OnNewEngineMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewEngineMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var engine = System.Text.Json.JsonSerializer.Deserialize<EngineAddedEvent>(body);
            if (engine == null) return;
            var item = new Item
            {
                Id = engine.EngineId,
                Name = engine.Name,
                ItemType = ItemType.Engine,
                ItemTypeId = engine.EngineId
            };
            await _itemsRepository.Add(item);
        }

        public void Stop()
        {
        }
    }
}
