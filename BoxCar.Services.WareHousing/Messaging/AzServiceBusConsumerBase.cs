using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.Repositories;
using Microsoft.Azure.ServiceBus;

namespace BoxCar.Services.WareHousing.Messaging
{
    public class AzServiceBusConsumerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly ItemsRepository _itemsRepository;
        protected readonly IMessageBus _messageBus;
        protected readonly string _subscriptionName;
        protected readonly string _connectionString;

        public AzServiceBusConsumerBase(IConfiguration configuration, IMessageBus messageBus, ItemsRepository itemsRepository)
        {
            _configuration = configuration;
            _itemsRepository = itemsRepository;
            _messageBus = messageBus;
            _subscriptionName = _configuration.GetValue<string>("SubscriptionName");
            _connectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
        }

        protected Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }
    }
}