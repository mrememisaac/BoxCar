using BoxCar.Integration.MessageBus;
using BoxCar.ShoppingBasket.Repositories.Consumers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.ShoppingBasket.Messaging.Messages;

namespace BoxCar.ShoppingBasket.Messaging
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
            var chassisEntity = await _chassisRepository.GetByIdAsync(chassisAddedEvent.ChassisId);
            if (chassisEntity != null)
            {
                return;
            }
            var chassis = new Entities.Chassis
            {
                Id = chassisAddedEvent.ChassisId,
                Name = chassisAddedEvent.Name,
                Description = chassisAddedEvent.Description,
                Price = chassisAddedEvent.Price
            };
            await _chassisRepository.CreateAsync(chassis);
        }

        public void Stop()
        {
        }
    }
}
