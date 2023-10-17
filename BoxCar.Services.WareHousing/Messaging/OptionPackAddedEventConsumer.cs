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
    public class OptionPackAddedEventConsumer : AzServiceBusConsumerBase, IOptionPackAzServiceBusConsumer
    {
        private readonly string _optionPackAddedEventTopic;
        private readonly IReceiverClient _optionPackAddedMessageReceiverClient;

        public OptionPackAddedEventConsumer(IConfiguration configuration, IMessageBus messageBus, ItemsRepository itemsRepository, ILoggerFactory loggerFactory)
            : base(configuration, messageBus, itemsRepository, loggerFactory)
        {
            _optionPackAddedEventTopic = _configuration.GetValue<string>("OptionPackAddedEventTopic");
            _optionPackAddedMessageReceiverClient = new SubscriptionClient(_connectionString, _optionPackAddedEventTopic, _subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };
            _optionPackAddedMessageReceiverClient.RegisterMessageHandler(OnNewOptionPackMessageReceived, messageHandlerOptions);
        }

        private async Task OnNewOptionPackMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);

            var optionPack = System.Text.Json.JsonSerializer.Deserialize<OptionPackAddedEvent>(body);
            if (optionPack == null) return;
            var item = new Item
            {
                Id = optionPack.OptionPackId,
                Name = optionPack.Name,
                ItemType = ItemType.OptionPack,
                ItemTypeId = optionPack.OptionPackId
            };
            await _itemsRepository.Add(item);
            var items = optionPack.Options.Select(o => new Item
            {
                Id = o.OptionId,
                Name = o.Name,
                ItemType = ItemType.Option,
                ItemTypeId = o.OptionId
            });
            await _itemsRepository.Add(items.AsEnumerable());
        }

        public void Stop()
        {
        }
    }
}
