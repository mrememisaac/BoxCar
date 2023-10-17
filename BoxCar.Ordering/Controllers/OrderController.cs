using BoxCar.Integration.MessageBus;
using BoxCar.Ordering.Messages;
using BoxCar.Ordering.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BoxCar.Ordering.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBus _messageBus;
        private readonly ILogger<OrderController> _logger;
        private readonly string _orderCancellationRequestTopic;

        public OrderController(IOrderRepository orderRepository, IMessageBus messageBus, 
            ILogger<OrderController> logger,
            IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _messageBus = messageBus;
            _logger = logger;
            _orderCancellationRequestTopic = configuration.GetValue<string>("OrderCancellationRequest") ?? "order_cancellation_request";
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> List(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersForUser(userId);
            return Ok(orders);
        }

        [HttpPost("cancel/{orderId}")]
        public async Task<IActionResult> Cancel(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null) return NotFound();
            if(order.FulfillmentStatus == Entities.FulfillmentStatus.Collected)
            {
                return BadRequest(new { Message = "Cannot cancel an order that has already been collected " });
            }
            await _orderRepository.CancelOrder(order);
            try
            {
                await _messageBus.PublishMessage(new OrderCancellationRequest { OrderId = orderId }, _orderCancellationRequestTopic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while publishing an order cancellation message for  order {0}", orderId);
            }
            return Ok(order);
        }
    }
}
