using BoxCar.Integration.MessageBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.Catalogue.Messages;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using BoxCar.Catalogue.Core.Contracts.Messaging;
using Microsoft.Extensions.Logging;
using BoxCar.Catalogue.Persistence.Repositories;

namespace BoxCar.Catalogue.Messaging
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
            var vehicleEnty = await _vehicleRepository.GetByIdAsync(vehicleAddedEvent.VehicleId, token);
            if (vehicleEnty != null)
            {
                return;
            }
            Domain.Chassis chassis = await GetChassis(vehicleAddedEvent, token);
            Domain.Engine engine = await GetEngine(vehicleAddedEvent, token);
            Domain.OptionPack optionPack = await GetOptionPack(vehicleAddedEvent, token);
            var vehicle = new Domain.Vehicle(vehicleAddedEvent.VehicleId, engine, chassis, optionPack, vehicleAddedEvent.Price);
            await _vehicleRepository.CreateAsync(vehicle, token);
        }

        async Task<Domain.Engine> GetEngine(VehicleAddedEvent vehicleAddedEvent, CancellationToken token)
        {
            var engine = await _engineRepository.GetByIdAsync(vehicleAddedEvent.Engine.Id, token);
            if (engine == null)
            {
                return
                await _engineRepository.CreateAsync(
                    new Domain.Engine(vehicleAddedEvent.Engine.Id,
                        vehicleAddedEvent.Engine.Name, vehicleAddedEvent.Engine.FuelType, vehicleAddedEvent.Engine.IgnitionMethod,
                        vehicleAddedEvent.Engine.Strokes, vehicleAddedEvent.Engine.Price), token);
            }
            return engine;
        }

        async Task<Domain.Chassis> GetChassis(VehicleAddedEvent vehicleAddedEvent, CancellationToken token)
        {
            var chassis = await _chassisRepository.GetByIdAsync(vehicleAddedEvent.Chassis.Id, token);
            if (chassis == null)
            {
                return await _chassisRepository.CreateAsync(new Domain.Chassis(vehicleAddedEvent.Chassis.Id,
                    vehicleAddedEvent.Chassis.Name, vehicleAddedEvent.Chassis.Description, vehicleAddedEvent.Chassis.Price), token);
            }
            return chassis;
        }

        async Task<Domain.OptionPack> GetOptionPack(VehicleAddedEvent vehicleAddedEvent, CancellationToken token)
        {
            var optionPack = await _optionPackRepository.GetByIdAsync(vehicleAddedEvent.OptionPack.Id, token);
            if (optionPack == null)
            {
                optionPack = new Domain.OptionPack(vehicleAddedEvent.OptionPack.Id, vehicleAddedEvent.OptionPack.Name);
                vehicleAddedEvent.OptionPack.Options.ForEach(option =>
                        optionPack.AddOption(new Domain.Option(option.OptionId, option.Name, option.Value, option.Price))
                        );
            }
            return optionPack;
        }

        public void Stop()
        {
        }
    }
}