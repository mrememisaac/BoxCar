using BoxCar.Integration.MessageBus;
using BoxCar.Catalogue.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using Microsoft.Extensions.Configuration;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Core.Contracts.Messaging;
using Microsoft.Extensions.Logging;
using BoxCar.Catalogue.Persistence.Repositories;

namespace BoxCar.Catalogue.Messaging
{
    public class ChassisAddedEventConsumer : AzServiceBusConsumerBase, IChassisAzServiceBusConsumer
    {
        private readonly string _chassisAddedEventTopic;
        private readonly IReceiverClient _chassisAddedMessageReceiverClient;
        private readonly ChassisRepository _chassisRepository;

        public ChassisAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus,
            ChassisRepository chassisRepository, 
            ILoggerFactory loggerFactory)
            : base(configuration, messageBus, loggerFactory)
        {
            _chassisAddedEventTopic = _configuration.GetValue<string>("ChassisAddedEvent");
            _chassisAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _chassisAddedEventTopic, _subscriptionName);
            _chassisRepository = chassisRepository;
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _chassisAddedMessageReceiverClient.RegisterMessageHandler(OnNewChassisMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewChassisMessageReceived(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var chassisAddedEvent = System.Text.Json.JsonSerializer.Deserialize<ChassisAddedEvent>(body);
            if (chassisAddedEvent == null) return;
            var chassisEntity = await _chassisRepository.GetByIdAsync(chassisAddedEvent.ChassisId, token);
            if (chassisEntity != null)
            {
                return;
            }
            var chassis = new Domain.Chassis(chassisAddedEvent.ChassisId, 
                chassisAddedEvent.Name, chassisAddedEvent.Description, chassisAddedEvent.Price);
            await _chassisRepository.CreateAsync(chassis, token);
        }

        public void Stop()
        {
        }
    }
}
