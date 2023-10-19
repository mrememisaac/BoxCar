﻿using BoxCar.Integration.MessageBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;
using BoxCar.Catalogue.Messages;
using Microsoft.Extensions.Configuration;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Core.Contracts.Messaging;
using Microsoft.Extensions.Logging;
using BoxCar.Catalogue.Persistence.Repositories;

namespace BoxCar.Catalogue.Messaging
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
            var engineEntity = await _optionPackRepository.GetByIdAsync(optionPackInfo.OptionPackId, token);
            if (engineEntity != null)
            {
                return;
            }
            var optionPack = new Domain.OptionPack(optionPackInfo.OptionPackId, optionPackInfo.Name);
            optionPackInfo.Options.ForEach(option =>
                    optionPack.AddOption(new Domain.Option(option.OptionId, option.Name, option.Value, option.Price))
                    );
            await _optionPackRepository.CreateAsync(optionPack, token);
        }

        public void Stop()
        {
        }
    }
}
