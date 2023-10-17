using BoxCar.Integration.MessageBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.Catalogue.Messages;
using Microsoft.Extensions.Configuration;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Core.Contracts.Messaging;
using Microsoft.Extensions.Logging;

namespace BoxCar.Catalogue.Messaging
{

    public class EngineAddedEventConsumer : AzServiceBusConsumerBase, IEngineAzServiceBusConsumer
    {
        private readonly string _engineAddedEventTopic;
        private readonly IReceiverClient _engineAddedMessageReceiverClient;
        private readonly IEngineRepository _engineRepository;

        public EngineAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus, 
            IEngineRepository engineRepository,
            ILoggerFactory loggerFactory)
            : base(configuration, messageBus, loggerFactory)
        {
            _engineAddedEventTopic = _configuration.GetValue<string>("EngineAddedEventTopic");
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

            var engine = new Domain.Engine(engineAddedEvent.EngineId, engineAddedEvent.Name, engineAddedEvent.FuelType, 
                engineAddedEvent.IgnitionMethod, engineAddedEvent.Strokes, engineAddedEvent.Price);
            await _engineRepository.CreateAsync(engine, token);
        }

        public void Stop()
        {
        }
    }
}
