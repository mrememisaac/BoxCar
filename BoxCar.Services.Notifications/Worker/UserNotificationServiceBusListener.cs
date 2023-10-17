using BoxCar.Integration.MessageBus;
using BoxCar.Services.Notifications.Messages;
using BoxCar.Services.Payment.Models;
using BoxCar.Services.Payment.Services;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace BoxCar.Services.Payment.Worker
{
    public class OrderStatusUpdateMessageServiceBusListener : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private ISubscriptionClient _subscriptionClient;
        private readonly IEmailGatewayService _emailGatewayService;
        private readonly IMessageBus _messageBus;

        public OrderStatusUpdateMessageServiceBusListener(IConfiguration configuration, ILoggerFactory loggerFactory,
            IEmailGatewayService emailGatewayService, IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<OrderStatusUpdateMessageServiceBusListener>();
            _emailGatewayService = emailGatewayService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriptionClient = new SubscriptionClient(_configuration.GetValue<string>("ServiceBusConnectionString"),
                _configuration.GetValue<string>("OrderStatusUpdateMessageTopic"), _configuration.GetValue<string>("subscriptionName"));

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"ServiceBusListener stopping.");
            await _subscriptionClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
            _logger.LogError(e, "Error while processing queue item in ServiceBusListener.");
        }

        protected async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            OrderStatusUpdateMessage orderStatusUpdateMessage = System.Text.Json.JsonSerializer.Deserialize<OrderStatusUpdateMessage>(messageBody);

            var emailInfo = new EmailInfo
            {
                Email = orderStatusUpdateMessage.Email,
                Message = orderStatusUpdateMessage.Message,
                OrderId = orderStatusUpdateMessage.OrderId
            };

            var result = await _emailGatewayService.SendEmail(emailInfo);

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            _logger.LogDebug($"{orderStatusUpdateMessage.OrderId}: ServiceBusListener received item.");
            await Task.Delay(20000);
            _logger.LogDebug($"{orderStatusUpdateMessage.OrderId}:  ServiceBusListener processed item.");
        }
    }
}
