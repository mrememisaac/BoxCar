using BoxCar.Integration.MessageBus;
using BoxCar.ShoppingBasket.Repositories.Consumers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.ShoppingBasket.Messaging.Messages;
using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Messaging
{
    public class VehicleAddedEventConsumer : AzServiceBusConsumerBase, IVehicleAzServiceBusConsumer
    {
        private readonly string _vehicleAddedEventTopic;
        private readonly IReceiverClient _vehicleAddedMessageReceiverClient;
        private readonly VehicleRepository _vehicleRepository;
        private readonly EngineRepository _engineRepository;
        private readonly OptionPackRepository _optionPackRepository;
        private readonly ChassisRepository _chassisRepository;

        public VehicleAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus,
            VehicleRepository vehicleRepository,
            EngineRepository engineRepository,
            OptionPackRepository optionPackRepository,
            ChassisRepository chassisRepository,
            ILoggerFactory loggerFactory)
            : base(configuration, messageBus, loggerFactory)
        {
            _vehicleAddedEventTopic = _configuration.GetValue<string>("VehicleAddedEvent");
            _vehicleAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _vehicleAddedEventTopic, _subscriptionName);
            _vehicleRepository = vehicleRepository;
            _engineRepository = engineRepository;
            _optionPackRepository = optionPackRepository;
            _chassisRepository = chassisRepository;
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _vehicleAddedMessageReceiverClient.RegisterMessageHandler(OnNewVehicleMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewVehicleMessageReceived(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var vehicleAddedEvent = System.Text.Json.JsonSerializer.Deserialize<VehicleAddedEvent>(body);

            if (vehicleAddedEvent == null) return;
            var vehicleEnty = await _vehicleRepository.GetByIdAsync(vehicleAddedEvent.VehicleId);
            if (vehicleEnty != null)
            {
                return;
            }
            Chassis chassis = await GetChassis(vehicleAddedEvent, token);
            Engine engine = await GetEngine(vehicleAddedEvent, token);
            OptionPack optionPack = await GetOptionPack(vehicleAddedEvent, token);
            var vehicle = new Vehicle
            {
                Id = vehicleAddedEvent.VehicleId,
                Name = vehicleAddedEvent.Name,
                EngineId = engine.Id,
                ChassisId = chassis.Id,
                OptionPackId = optionPack.Id,
                Price = vehicleAddedEvent.Price
            };
            await _vehicleRepository.CreateAsync(vehicle);
        }

        async Task<Engine> GetEngine(VehicleAddedEvent vehicleAddedEvent, CancellationToken token)
        {
            var engine = await _engineRepository.GetByIdAsync(vehicleAddedEvent.Engine.Id);
            if (engine == null)
            {
                return
                await _engineRepository.CreateAsync(
                    new Engine
                    {
                        Id = vehicleAddedEvent.Engine.Id,
                        Name = vehicleAddedEvent.Engine.Name,
                        FuelType = vehicleAddedEvent.Engine.FuelType,
                        IgnitionMethod = vehicleAddedEvent.Engine.IgnitionMethod,
                        Strokes = vehicleAddedEvent.Engine.Strokes,
                        Price = vehicleAddedEvent.Engine.Price
                    });
            }
            return engine;
        }

        async Task<Chassis> GetChassis(VehicleAddedEvent vehicleAddedEvent, CancellationToken token)
        {
            var chassis = await _chassisRepository.GetByIdAsync(vehicleAddedEvent.Chassis.Id);
            if (chassis == null)
            {
                return await _chassisRepository.CreateAsync(new Chassis
                {
                    Id = vehicleAddedEvent.Chassis.Id,
                    Name = vehicleAddedEvent.Chassis.Name,
                    Description =
                    vehicleAddedEvent.Chassis.Description,
                    Price = vehicleAddedEvent.Chassis.Price
                });
            }
            return chassis;
        }

        async Task<OptionPack> GetOptionPack(VehicleAddedEvent vehicleAddedEvent, CancellationToken token)
        {
            var optionPack = await _optionPackRepository.GetByIdAsync(vehicleAddedEvent.OptionPack.Id);
            if (optionPack == null)
            {
                optionPack = new OptionPack
                {
                    Id = vehicleAddedEvent.OptionPack.OptionPackId,
                    Name = vehicleAddedEvent.OptionPack.Name
                };
                vehicleAddedEvent.OptionPack.Options.ForEach(option =>
                        optionPack.Options.Add(new Option { Id = option.OptionId, Name = option.Name, Value = option.Value, Price = option.Price })
                        );
            }
            return optionPack;
        }

        public void Stop()
        {
        }
    }
}