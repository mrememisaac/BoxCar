using External.PaymentGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace External.PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {      
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(ILogger<PaymentsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult TryPayment([FromBody] PaymentDto payment)
        {

            int num = new Random().Next(1000);
            if (num > 500)
                return Ok(true);

            return Ok(false);
        }
    }
}