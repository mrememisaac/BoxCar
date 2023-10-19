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
    public class VehicleAddedEventConsumer : AzServiceBusConsumerBase, IVehicleAzServiceBusConsumer
    {
        private readonly string _vehicleAddedEventTopic;
        private readonly IReceiverClient _vehicleAddedMessageReceiverClient;

        public VehicleAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus, ItemsRepository itemsRepository, ILoggerFactory loggerFactory)
            : base(configuration, messageBus, itemsRepository, loggerFactory)
        {
            _vehicleAddedEventTopic = configuration.GetValue<string>("VehicleAddedEvent");
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
            if (vehicle == null) return;
            var localCopy = _itemsRepository.GetByItemTypeAndItemTypeId(ItemType.Vehicle, vehicle.VehicleId);
            if (localCopy != null) return;
            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = vehicle.Name,
                ItemType = ItemType.Vehicle,
                ItemTypeId = vehicle.VehicleId,
                SpecificationKey =
                SpecificationKeyGenerator.GenerateSpecificationKey(vehicle.VehicleId, vehicle.Chassis.ChassisId, vehicle.Engine.EngineId, vehicle.OptionPack.OptionPackId)
            };
            await _itemsRepository.Add(item);
        }

        public void Stop()
        {
        }
    }
}
