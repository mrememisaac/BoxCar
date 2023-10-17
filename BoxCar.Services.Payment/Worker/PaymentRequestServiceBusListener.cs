﻿using BoxCar.Integration.MessageBus;
using BoxCar.Services.Payment.Messages;
using BoxCar.Services.Payment.Models;
using BoxCar.Services.Payment.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoxCar.Services.Payment.Worker
{
    public class PaymentRequestServiceBusListener : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private ISubscriptionClient _subscriptionClient;
        private readonly IExternalGatewayPaymentService _externalGatewayPaymentService;
        private readonly IMessageBus _messageBus;
        private readonly string _orderPaymentUpdatedMessageTopic;

        public PaymentRequestServiceBusListener(IConfiguration configuration, ILoggerFactory loggerFactory, 
            IExternalGatewayPaymentService externalGatewayPaymentService, IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<PaymentRequestServiceBusListener>();
            _externalGatewayPaymentService = externalGatewayPaymentService;
            _orderPaymentUpdatedMessageTopic = configuration.GetValue<string>("OrderPaymentUpdate");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriptionClient = new SubscriptionClient(_configuration.GetValue<string>("ServiceBusConnectionString"), 
                _configuration.GetValue<string>("OrderPaymentRequest"), _configuration.GetValue<string>("subscriptionName"));

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
            OrderPaymentRequestMessage orderPaymentRequestMessage = JsonConvert.DeserializeObject<OrderPaymentRequestMessage>(messageBody);

            PaymentInfo paymentInfo = new PaymentInfo
            {
                CardNumber = orderPaymentRequestMessage.CardNumber,
                CardName = orderPaymentRequestMessage.CardName,
                CardExpiration = orderPaymentRequestMessage.CardExpiration,
                Total = orderPaymentRequestMessage.Total
            };

            var result = await _externalGatewayPaymentService.PerformPayment(paymentInfo);

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            //send payment result to order service via service bus
            OrderPaymentUpdateMessage orderPaymentUpdateMessage = new OrderPaymentUpdateMessage
            {
                PaymentSuccess = result, 
                OrderId = orderPaymentRequestMessage.OrderId
            };

            try
            {
                await _messageBus.PublishMessage(orderPaymentUpdateMessage, _orderPaymentUpdatedMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _logger.LogDebug($"{orderPaymentRequestMessage.OrderId}: ServiceBusListener received item.");
            await Task.Delay(20000);
            _logger.LogDebug($"{orderPaymentRequestMessage.OrderId}:  ServiceBusListener processed item.");
        }
    }
}
