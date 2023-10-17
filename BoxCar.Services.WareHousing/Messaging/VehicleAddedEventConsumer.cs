using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.Repositories;
using BoxCar.Services.WareHousing.Entities;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Messaging
{
    public class VehicleAddedEventConsumer : AzServiceBusConsumerBase, IAzServiceBusConsumer
    {
        private readonly string _vehicleAddedEventTopic;
        private readonly IReceiverClient _vehicleAddedMessageReceiverClient;

        public VehicleAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus, ItemsRepository itemsRepository)
            : base(configuration, messageBus, itemsRepository)
        {
            _vehicleAddedEventTopic = _configuration.GetValue<string>("VehicleAddedEventTopic");
            _vehicleAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _vehicleAddedEventTopic, _subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _vehicleAddedMessageReceiverClient.RegisterMessageHandler(OnNewVehicleMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewVehicleMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var vehicle = System.Text.Json.JsonSerializer.Deserialize<VehicleAddedEvent>(body);

            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = vehicle.Name,
                ItemType = "Vehicle",
                ItemTypeId = vehicle.VehicleId
            };
            await _itemsRepository.Add(item);
        }

        public void Stop()
        {
        }
    }
}
