using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BoxCar.Integration.MessageBus;
using BoxCar.ShoppingBasket.Messages;
using BoxCar.ShoppingBasket.Models;
using BoxCar.ShoppingBasket.Repositories;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;

namespace BoxCar.ShoppingBasket.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;
        private readonly string _checkoutRequestTopic;
        private readonly ILogger _logger;
        public BasketsController(IBasketRepository basketRepository, 
            IConfiguration configuration,
            ILogger<BasketsController> logger,
            IMapper mapper, IMessageBus messageBus)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _messageBus = messageBus;
            _checkoutRequestTopic = configuration.GetValue<string>("CheckoutRequest") ?? "checkout_request";
            _logger = logger;
        }

        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid basketId)
        {
            var basket = await _basketRepository.GetBasketById(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Basket>(basket);
            result.NumberOfItems = basket.BasketLines.Sum(bl => bl.Quantity);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketForCreation basketForCreation)
        {
            var basketEntity = _mapper.Map<Entities.Basket>(basketForCreation);

            _basketRepository.AddBasket(basketEntity);
            await _basketRepository.SaveChanges();

            var basketToReturn = _mapper.Map<Basket>(basketEntity);

            return CreatedAtRoute(
                "GetBasket",
                new { basketId = basketEntity.BasketId },
                basketToReturn);
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CheckoutBasketAsync([FromBody] BasketCheckout basketCheckout)
        {
            using (_logger.BeginScope("Basket Checkout initiated by user for baket #{}", basketCheckout.BasketId))
            {
                try
                {
                    //based on basket checkout, fetch the basket lines from repo
                    var basket = await _basketRepository.GetBasketById(basketCheckout.BasketId);

                    if (basket == null)
                    {
                        return BadRequest();
                    }

                    BasketCheckoutMessage basketCheckoutMessage = _mapper.Map<BasketCheckoutMessage>(basketCheckout);
                    basketCheckoutMessage.BasketLines = new List<BasketLineMessage>();

                    foreach (var b in basket.BasketLines)
                    {
                        var basketLineMessage = new BasketLineMessage
                        {
                            BasketLineId = b.BasketLineId,
                            ChassisId = b.ChassisId,
                            EngineId = b.EngineId,
                            OptionPackId = b.OptionPackId,
                            VehicleId = b.VehicleId,
                            Quantity = b.Quantity,
                            Price = b.UnitPrice,
                        };
                        basketCheckoutMessage.BasketLines.Add(basketLineMessage);
                    }

                    try
                    {
                        await _messageBus.PublishMessage(basketCheckoutMessage, _checkoutRequestTopic);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    await _basketRepository.ClearBasket(basketCheckout.BasketId);
                    return Accepted(basketCheckoutMessage);
                }
                catch (BrokenCircuitException ex)
                {
                    string message = ex.Message;
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
                }
            }
        }
    }
}
