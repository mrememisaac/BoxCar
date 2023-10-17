﻿using BoxCar.Integration.MessageBus;
using BoxCar.Ordering.Entities;
using BoxCar.Ordering.Messages;
using BoxCar.Ordering.Repositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoxCar.Ordering.Messaging
{
    public class AzServiceBusConsumer: IAzServiceBusConsumer
    {
        private readonly string subscriptionName;
        private readonly IReceiverClient checkoutMessageReceiverClient;
        private readonly IReceiverClient orderPaymentUpdateMessageReceiverClient;

        private readonly IConfiguration _configuration;

        private readonly OrderRepository _orderRepository;
        private readonly IMessageBus _messageBus;

        private readonly string checkoutMessageTopic;
        private readonly string _orderPaymentRequestMessageTopic;
        private readonly string _orderPaymentUpdatedMessageTopic;
        private readonly ILogger _logger;
        private readonly string _orderStatusUpdateMessageTopic;
        private readonly string _fulfillOrderRequestMessageTopic;

        public AzServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, OrderRepository orderRepository, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _logger = loggerFactory.CreateLogger<AzServiceBusConsumer>();
            _messageBus = messageBus;

            var serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("SubscriptionName"); 
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            _orderStatusUpdateMessageTopic = _configuration.GetValue<string>("OrderStatusUpdateMessageTopic");
            _orderPaymentRequestMessageTopic = _configuration.GetValue<string>("OrderPaymentRequestMessageTopic");
            _orderPaymentUpdatedMessageTopic = _configuration.GetValue<string>("OrderPaymentUpdatedMessageTopic");
            _fulfillOrderRequestMessageTopic = _configuration.GetValue<string>("FulfillOrderRequestMessageTopic");

            _checkoutMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, checkoutMessageTopic, subscriptionName);
            _orderPaymentUpdateMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, _orderPaymentUpdatedMessageTopic, subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };

            _checkoutMessageReceiverClient.RegisterMessageHandler(OnCheckoutMessageReceived, messageHandlerOptions);
            _orderPaymentUpdateMessageReceiverClient.RegisterMessageHandler(OnOrderPaymentUpdateReceived, messageHandlerOptions);
        }

        private async Task OnCheckoutMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);//json from service bus

            //save order with status not paid
            BasketCheckoutMessage basketCheckoutMessage = JsonConvert.DeserializeObject<BasketCheckoutMessage>(body);

            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                UserId = basketCheckoutMessage.UserId,
                Id = orderId,
                OrderPaid = false,
                OrderPlaced = DateTime.Now,
                OrderTotal = basketCheckoutMessage.BasketTotal
            };

            await _orderRepository.AddOrder(order);

            //send order payment request message
            OrderPaymentRequestMessage orderPaymentRequestMessage = new OrderPaymentRequestMessage
            {
                CardExpiration = basketCheckoutMessage.CardExpiration,
                CardName = basketCheckoutMessage.CardName,
                CardNumber = basketCheckoutMessage.CardNumber,
                OrderId = orderId,
                Total = basketCheckoutMessage.BasketTotal
            };

            try
            {
                await _messageBus.PublishMessage(orderPaymentRequestMessage, orderPaymentRequestMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var orderReceived = new OrderStatusUpdateMessage
            {
                OrderId = orderId,
                Message = $"Thank you for your order ({orderId}). We appreciate your business",
                CreationDateTime = DateTime.Now,
                Email = basketCheckoutMessage.Email,
                UserId = basketCheckoutMessage.UserId,
            };

            try
            {
                await _messageBus.PublishMessage(orderReceived, _orderStatusUpdateMessageTopic);
        }

        private async Task OnOrderPaymentUpdateReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            OrderPaymentUpdateMessage orderPaymentUpdateMessage = System.Text.Json.JsonSerializer.Deserialize<OrderPaymentUpdateMessage>(body);

            await _orderRepository.UpdateOrderPaymentStatus(orderPaymentUpdateMessage.OrderId, orderPaymentUpdateMessage.PaymentSuccess);
            var order = await _orderRepository.GetOrderById(orderPaymentUpdateMessage.OrderId);

            if (orderPaymentUpdateMessage.PaymentSuccess)
            {
                var fulfillOrderRequest = new FulfillOrderRequest
                {
                    OrderId = orderPaymentUpdateMessage.OrderId,
                    OrderItems = order.OrderLines.Select(l => new FulfillOrderRequestLine
                    {
                        ChassisId = l.ChassisId,
                        EngineId = l.EngineId,
                        OptionPackId = l.OptionPackId,
                        OrderId = order.Id,
                        Quantity = l.Quantity,
                        VehicleId = l.VehicleId,
                    })
                    .ToList(),
                };

                try
                {
                    await _messageBus.PublishMessage(fulfillOrderRequest, _fulfillOrderRequestMessageTopic);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error arose while publishing user payment failure notification for User {0} for Order {1}", order.UserId, order.Id);
                }
            }
            else
            {
                var orderReceived = new OrderStatusUpdateMessage
                {
                    OrderId = orderPaymentUpdateMessage.OrderId,
                    Message = $"We were unable to process payment for order ({orderPaymentUpdateMessage.OrderId}). Please try again.",
                    CreationDateTime = DateTime.Now,
                    UserId = order.UserId
                };

                try
                {
                    await _messageBus.PublishMessage(orderReceived, _orderStatusUpdateMessageTopic);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error arose while publishing user payment failure notification for User {0} for Order {1}", order.UserId, order.Id);
                }
            }
        }

        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }

        public void Stop()
        {
        }
    }
}
