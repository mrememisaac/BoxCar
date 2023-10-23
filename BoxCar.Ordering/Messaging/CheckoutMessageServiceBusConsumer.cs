using BoxCar.Integration.MessageBus;
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
    public class CheckoutMessageServiceBusConsumer : IAzServiceBusConsumer
    {
        private readonly string subscriptionName;
        private readonly IReceiverClient _checkoutMessageReceiverClient;
        private readonly IReceiverClient _orderPaymentUpdateMessageReceiverClient;
        private readonly IReceiverClient _orderItemsAvailabilityUpdateMessageReceiverClient;

        private readonly IConfiguration _configuration;

        private readonly OrderRepository _orderRepository;
        private readonly IMessageBus _messageBus;

        private readonly string checkoutMessageTopic;
        private readonly string _orderPaymentRequestMessageTopic;
        private readonly string _orderPaymentUpdatedMessageTopic;
        private readonly ILogger _logger;
        private readonly string _orderStatusUpdateMessageTopic;
        private readonly string _fulfillOrderRequestMessageTopic;
        private readonly string _orderItemsAvailabilityUpdateMessageTopic;

        public CheckoutMessageServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, OrderRepository orderRepository, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _logger = loggerFactory.CreateLogger<CheckoutMessageServiceBusConsumer>();
            _messageBus = messageBus;

            var serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("SubscriptionName");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutRequest");
            _orderStatusUpdateMessageTopic = _configuration.GetValue<string>("OrderStatusUpdate");
            _orderPaymentRequestMessageTopic = _configuration.GetValue<string>("OrderPaymentRequest");
            _orderPaymentUpdatedMessageTopic = _configuration.GetValue<string>("OrderPaymentUpdate");
            _fulfillOrderRequestMessageTopic = _configuration.GetValue<string>("OrderFullfillmentRequest");
            _orderItemsAvailabilityUpdateMessageTopic = _configuration.GetValue<string>("OrderItemsAvailabilityUpdate");

            _checkoutMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, checkoutMessageTopic, subscriptionName);
            _orderPaymentUpdateMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, _orderPaymentUpdatedMessageTopic, subscriptionName);
            _orderItemsAvailabilityUpdateMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, _orderItemsAvailabilityUpdateMessageTopic, subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };

            _checkoutMessageReceiverClient.RegisterMessageHandler(OnCheckoutMessageReceived, messageHandlerOptions);
            _orderPaymentUpdateMessageReceiverClient.RegisterMessageHandler(OnOrderPaymentUpdateReceived, messageHandlerOptions);
            _orderItemsAvailabilityUpdateMessageReceiverClient.RegisterMessageHandler(OnItemAvailabilityUpdateReceived, messageHandlerOptions);
        }

        private async Task OnCheckoutMessageReceived(Message message, CancellationToken arg2)
        {

            var body = Encoding.UTF8.GetString(message.Body);

            //save order with status not paid
            BasketCheckoutMessage basketCheckoutMessage = JsonConvert.DeserializeObject<BasketCheckoutMessage>(body);
            using (_logger.BeginScope("Started processing order by user {0}", basketCheckoutMessage.UserId))
            {
                Guid orderId = await SaveOrder(basketCheckoutMessage);

                await SendPaymentRequest(basketCheckoutMessage, orderId);

                await ThankUserForOrder(basketCheckoutMessage, orderId);
            }
        }

        private async Task ThankUserForOrder(BasketCheckoutMessage basketCheckoutMessage, Guid orderId)
        {
            _logger.LogInformation("Thanking user for order #{0}", orderId);
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
            catch (Exception e)
            {
                _logger.LogError(e, "Error arose while handling checkout message by User {0} for Order {1}", basketCheckoutMessage.UserId, orderId);
            }
        }

        private async Task SendPaymentRequest(BasketCheckoutMessage basketCheckoutMessage, Guid orderId)
        {
            _logger.LogInformation("requesting payment for order #{0}", orderId);
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
                await _messageBus.PublishMessage(orderPaymentRequestMessage, _orderPaymentRequestMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<Guid> SaveOrder(BasketCheckoutMessage basketCheckoutMessage)
        {
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                UserId = basketCheckoutMessage.UserId,
                Id = orderId,
                OrderPaid = false,
                OrderPlaced = DateTime.Now,
                OrderTotal = basketCheckoutMessage.BasketTotal
            };
            basketCheckoutMessage.BasketLines.ForEach(l => order.OrderLines.Add(new OrderLine
            {
                ChassisId = l.ChassisId,
                EngineId = l.EngineId,
                OptionPackId = l.OptionPackId,
                Quantity = l.Quantity,
                VehicleId = l.VehicleId,
                UnitPrice = l.Price
            }));
            await _orderRepository.AddOrder(order);
            return orderId;
        }

        private async Task OnOrderPaymentUpdateReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            OrderPaymentUpdateMessage orderPaymentUpdateMessage = System.Text.Json.JsonSerializer.Deserialize<OrderPaymentUpdateMessage>(body);

            Order order = await UpdateOrderPaymentStatus(orderPaymentUpdateMessage);

            await InstructWarehouseToFulfillOrder(orderPaymentUpdateMessage, order);

            await NotifyUserOfPaymentFailure(orderPaymentUpdateMessage, order);
        }

        private async Task InstructWarehouseToFulfillOrder(OrderPaymentUpdateMessage orderPaymentUpdateMessage, Order order)
        {
            if (!orderPaymentUpdateMessage.PaymentSuccess) return;

            var fulfillOrderRequest = new FulfillOrderRequest
            {
                OrderId = order.Id,
                OrderItems = order.OrderLines.Select(l => new FulfillOrderRequestLine
                {
                    OrderItemId = l.Id,
                    ChassisId = l.ChassisId,
                    EngineId = l.EngineId,
                    OptionPackId = l.OptionPackId,
                    OrderId = order.Id,
                    Quantity = l.Quantity,
                    VehicleId = l.VehicleId,
                    UnitPrice = l.UnitPrice,
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


        private async Task NotifyUserOfPaymentFailure(OrderPaymentUpdateMessage orderPaymentUpdateMessage, Order order)
        {
            if (orderPaymentUpdateMessage.PaymentSuccess) return;

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

        private async Task<Order> UpdateOrderPaymentStatus(OrderPaymentUpdateMessage orderPaymentUpdateMessage)
        {
            await _orderRepository.UpdateOrderPaymentStatus(orderPaymentUpdateMessage.OrderId, orderPaymentUpdateMessage.PaymentSuccess);
            var order = await _orderRepository.GetOrderById(orderPaymentUpdateMessage.OrderId);
            return order;
        }

        private async Task OnItemAvailabilityUpdateReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            OrderItemsAvailabilityUpdate update = System.Text.Json.JsonSerializer.Deserialize<OrderItemsAvailabilityUpdate>(body);
            if (update == null) return;
            var order = await _orderRepository.GetOrderById(update.OrderId);
            var msg = new OrderStatusUpdateMessage
            {
                OrderId = update.OrderId,
                Message = $"Your order is readyc for pickup",
                CreationDateTime = DateTime.Now,
                UserId = update.UserId
            };
            if (update.Lines.Any(x => x.Status == OrderItemAvailabilityStatus.InProduction))
            {
                msg.Message = $"Some items in  your order are in production. You will receive further updates shortly";
            }
            try
            {
                await _messageBus.PublishMessage(msg, _orderStatusUpdateMessageTopic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error arose while a user update for User {0} for Order {1}", order.UserId, msg.OrderId);
            }
        }


        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError("A service bus exception occured for subscription {0}. Exception: {1}", subscriptionName, exceptionReceivedEventArgs);
            return Task.CompletedTask;
        }

        public void Stop()
        {
        }
    }
}
