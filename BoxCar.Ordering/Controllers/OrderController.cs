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

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
            return Ok(order);
        }
    }
}
