using BoxCar.Integration.MessageBus;
using BoxCar.ShoppingBasket.Repositories.Consumers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.ShoppingBasket.Messaging.Messages;
namespace BoxCar.ShoppingBasket.Messaging
{

    public class EngineAddedEventConsumer : AzServiceBusConsumerBase, IEngineAzServiceBusConsumer
    {
        private readonly string _engineAddedEventTopic;
        private readonly IReceiverClient _engineAddedMessageReceiverClient;
        private readonly EngineRepository _engineRepository;

        public EngineAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus,
            EngineRepository engineRepository,
            ILoggerFactory loggerFactory)
            : base(configuration, messageBus, loggerFactory)
        {
            _engineAddedEventTopic = _configuration.GetValue<string>("EngineAddedEvent");
            _engineAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _engineAddedEventTopic, _subscriptionName);
            _engineRepository = engineRepository;
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _engineAddedMessageReceiverClient.RegisterMessageHandler(OnNewEngineMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewEngineMessageReceived(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var engineAddedEvent = System.Text.Json.JsonSerializer.Deserialize<EngineAddedEvent>(body);
            if (engineAddedEvent == null) return;
            var engineEntity = await _engineRepository.GetByIdAsync(engineAddedEvent.EngineId);
            if (engineEntity != null)
            {
                return;
            }
            var engine = new Entities.Engine
            {
                Id = engineAddedEvent.EngineId,
                Name = engineAddedEvent.Name,
                FuelType = engineAddedEvent.FuelType,
                IgnitionMethod = engineAddedEvent.IgnitionMethod,
                Strokes = engineAddedEvent.Strokes,
                Price = engineAddedEvent.Price
            };
            await _engineRepository.CreateAsync(engine);
        }

        public void Stop()
        {
        }
    }
}
