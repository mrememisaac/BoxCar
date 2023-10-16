using BoxCar.Integration.MessageBus;
using BoxCar.Services.Payment.Messages;
using BoxCar.Services.Payment.Models;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace BoxCar.Services.Payment.Services
{
    public interface IExternalGatewayPaymentService
    {
        Task<bool> PerformPayment(PaymentInfo paymentInfo);
    }

    public class ServiceBusListener : IHostedService
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private ISubscriptionClient subscriptionClient;
        private readonly IExternalGatewayPaymentService externalGatewayPaymentService;
        private readonly IMessageBus messageBus;
        private readonly string orderPaymentUpdatedMessageTopic;

        public ServiceBusListener(IConfiguration configuration, ILoggerFactory loggerFactory, IExternalGatewayPaymentService externalGatewayPaymentService, IMessageBus messageBus)
        {
            logger = loggerFactory.CreateLogger<ServiceBusListener>();
            orderPaymentUpdatedMessageTopic = configuration.GetValue<string>("OrderPaymentUpdatedMessageTopic");

            this.configuration = configuration;
            this.externalGatewayPaymentService = externalGatewayPaymentService;
            this.messageBus = messageBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriptionClient = new SubscriptionClient(configuration.GetValue<string>("ServiceBusConnectionString"), configuration.GetValue<string>("OrderPaymentRequestMessageTopic"), configuration.GetValue<string>("subscriptionName"));

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug($"ServiceBusListener stopping.");
            await this.subscriptionClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
            logger.LogError(e, "Error while processing queue item in ServiceBusListener.");
        }

        protected async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            OrderPaymentRequestMessage orderPaymentRequestMessage = System.Text.Json.JsonSerializer.Deserialize<OrderPaymentRequestMessage>(messageBody);

            PaymentInfo paymentInfo = new PaymentInfo
            {
                CardNumber = orderPaymentRequestMessage.CardNumber,
                CardName = orderPaymentRequestMessage.CardName,
                CardExpiration = orderPaymentRequestMessage.CardExpiration,
                Total = orderPaymentRequestMessage.Total
            };

            var result = await externalGatewayPaymentService.PerformPayment(paymentInfo);

            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            //send payment result to order service via service bus
            OrderPaymentUpdateMessage orderPaymentUpdateMessage = new OrderPaymentUpdateMessage
            {
                PaymentSuccess = result,
                OrderId = orderPaymentRequestMessage.OrderId
            };

            try
            {
                await messageBus.PublishMessage(orderPaymentUpdateMessage, orderPaymentUpdatedMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            logger.LogDebug($"{orderPaymentRequestMessage.OrderId}: ServiceBusListener received item.");
            await Task.Delay(20000);
            logger.LogDebug($"{orderPaymentRequestMessage.OrderId}:  ServiceBusListener processed item.");
        }
    }
}
