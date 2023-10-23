using BoxCar.Integration.MessageBus;
using BoxCar.ShoppingBasket.Repositories.Consumers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.ShoppingBasket.Messaging.Messages;

namespace BoxCar.ShoppingBasket.Messaging
{
    public class OptionPackAddedEventConsumer : AzServiceBusConsumerBase, IOptionPackAzServiceBusConsumer
    {
        private readonly string _optionPackAddedEventTopic;
        private readonly IReceiverClient _optionPackAddedMessageReceiverClient;
        private readonly OptionPackRepository _optionPackRepository;

        public OptionPackAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus,
            OptionPackRepository optionPackRepository,
            ILoggerFactory loggerFactory)
            : base(configuration, messageBus, loggerFactory)
        {
            _optionPackAddedEventTopic = _configuration.GetValue<string>("OptionPackAddedEvent");
            _optionPackAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _optionPackAddedEventTopic, _subscriptionName);
            _optionPackRepository = optionPackRepository;
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _optionPackAddedMessageReceiverClient.RegisterMessageHandler(OnNewOptionPackMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewOptionPackMessageReceived(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var optionPackInfo = System.Text.Json.JsonSerializer.Deserialize<OptionPackAddedEvent>(body);
            if (optionPackInfo == null) return;
            var engineEntity = await _optionPackRepository.GetByIdAsync(optionPackInfo.OptionPackId);
            if (engineEntity != null)
            {
                return;
            }
            var optionPack = new Entities.OptionPack { Id = optionPackInfo.OptionPackId, Name = optionPackInfo.Name };
            optionPackInfo.Options.ForEach(option =>
                    optionPack.Options.Add(new Entities.Option { Id = option.OptionId, Name = option.Name, Value = option.Value, Price = option.Price })
                    );
            await _optionPackRepository.CreateAsync(optionPack);
        }

        public void Stop()
        {
        }
    }
}
