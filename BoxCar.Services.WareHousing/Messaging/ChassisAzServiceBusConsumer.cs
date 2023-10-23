using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.Repositories;
using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.Services.WareHousing.Contracts.Messaging;

namespace BoxCar.Services.WareHousing.Messaging
{
    public class ChassisAddedEventConsumer : AzServiceBusConsumerBase, IChassisAzServiceBusConsumer
    {
        private readonly string _chassisAddedEventTopic;
        private readonly IReceiverClient _chassisAddedMessageReceiverClient;

        public ChassisAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus, ItemsRepository itemsRepository, ILoggerFactory loggerFactory)
            : base(configuration, messageBus, itemsRepository, loggerFactory)
        {
            _chassisAddedEventTopic = configuration.GetValue<string>("ChassisAddedEvent");
            _chassisAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _chassisAddedEventTopic, _subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _chassisAddedMessageReceiverClient.RegisterMessageHandler(OnNewChassisMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewChassisMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var chassis = System.Text.Json.JsonSerializer.Deserialize<ChassisAddedEvent>(body);
            if (chassis == null) return;
            var localCopy = await _itemsRepository.GetByItemTypeAndItemTypeId(ItemType.Chassis, chassis.ChassisId);
            if (localCopy != null) return;
            var item = new Item
            {
                Id = chassis.Id,
                Name = chassis.Name,
                ItemType = ItemType.Chassis,
                ItemTypeId = chassis.ChassisId, 
                SpecificationKey = chassis.ChassisId.ToString() 
            };
            await _itemsRepository.Add(item);
        }

        public void Stop()
        {
        }
    }
}
