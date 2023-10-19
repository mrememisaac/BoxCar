using BoxCar.Integration.MessageBus;
using Microsoft.Azure.ServiceBus;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BoxCar.Catalogue.Messaging
{
    public class AzServiceBusConsumerBase
    {
        protected readonly IConfiguration _configuration;        
        protected readonly IMessageBus _messageBus;
        protected readonly string _subscriptionName;
        protected readonly string _connectionString;
        protected readonly ILogger _logger;

        public AzServiceBusConsumerBase(IConfiguration configuration, IMessageBus messageBus, ILoggerFactory loggerFactory)
        {            
            _logger = loggerFactory.CreateLogger<AzServiceBusConsumerBase>();
            _configuration = configuration;            
            _messageBus = messageBus;
            _subscriptionName = _configuration.GetValue<string>("SubscriptionName");
            _connectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
        }

        protected Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError("A service bus exception occured for subscription {0}. Exception: {1}", _subscriptionName, exceptionReceivedEventArgs);
            return Task.CompletedTask;
        }
    }
}