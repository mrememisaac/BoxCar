using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BoxCar.ShoppingBasket.Models;
using BoxCar.ShoppingBasket.Repositories;
using BoxCar.ShoppingBasket.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.ShoppingBasket.Controllers
{
    [Route("api/baskets/{basketId}/basketlines")]
    [ApiController]
    public class BasketLinesController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketChangeEventRepository _basketChangeEventRepository;
        private readonly IBasketLinesRepository _basketLinesRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleCatalogService _vehicleCatalogService;
        private readonly IEngineRepository _engineRepository;
        private readonly IEngineCatalogService _engineCatalogService;
        private readonly IChassisRepository _chassisRepository;
        private readonly IChassisCatalogService _chassisCatalogService;
        private readonly IOptionPackRepository _optionPackRepository;
        private readonly IOptionPackCatalogService _optionPackCatalogService;
        private readonly IMapper _mapper;

        public BasketLinesController(IBasketRepository basketRepository, 
            IBasketLinesRepository basketLinesRepository, IVehicleRepository eventRepository, 
            IVehicleCatalogService eventCatalogService, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _basketLinesRepository = basketLinesRepository;
            _vehicleRepository = eventRepository;
            _vehicleCatalogService = eventCatalogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketLine>>> Get(Guid basketId)
        {
            if (!await _basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLines = await _basketLinesRepository.GetBasketLines(basketId);
            return Ok(_mapper.Map<IEnumerable<BasketLine>>(basketLines));             
        }

        [HttpGet("{basketLineId}", Name = "GetBasketLine")]
        public async Task<ActionResult<BasketLine>> Get(Guid basketId, 
            Guid basketLineId)
        {
            if (!await _basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLine = await _basketLinesRepository.GetBasketLineById(basketLineId);
            if (basketLine == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BasketLine>(basketLine));
        }

        [HttpPost]
        public async Task<ActionResult<BasketLine>> Post(Guid basketId, 
            [FromBody] BasketLineForCreation basketLineForCreation)
        {
            if (!await _basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            if (!await _vehicleRepository.VehicleExists(basketLineForCreation.VehicleId))
            {
                var vehicleFromCatalog = await _vehicleCatalogService.GetVehicle(basketLineForCreation.VehicleId);
                _vehicleRepository.AddVehicle(vehicleFromCatalog);
                await _vehicleRepository.SaveChanges();
            }

            if (!await _engineRepository.EngineExists(basketLineForCreation.EngineId))
            {
                var engineFromCatalog = await _engineCatalogService.GetEngine(basketLineForCreation.EngineId);
                _engineRepository.AddEngine(engineFromCatalog);
                await _engineRepository.SaveChanges();
            }

            if (!await _chassisRepository.ChassisExists(basketLineForCreation.ChassisId))
            {
                var chassisFromCatalog = await _chassisCatalogService.GetChassis(basketLineForCreation.ChassisId);
                _chassisRepository.AddChassis(chassisFromCatalog);
                await _chassisRepository.SaveChanges();
            }

            if (!await _optionPackRepository.OptionPackExists(basketLineForCreation.OptionPackId))
            {
                var optionPackFromCatalog = await _optionPackCatalogService.GetOptionPack(basketLineForCreation.OptionPackId);
                _optionPackRepository.AddOptionPack(optionPackFromCatalog);
                await _optionPackRepository.SaveChanges();
            }

            var basketLineEntity = _mapper.Map<Entities.BasketLine>(basketLineForCreation);

            var processedBasketLine = await _basketLinesRepository.AddOrUpdateBasketLine(basketId, basketLineEntity);
            await _basketLinesRepository.SaveChanges();

            var basketLineToReturn = _mapper.Map<BasketLine>(processedBasketLine);

            return CreatedAtRoute(
                "GetBasketLine",
                new { basketId = basketLineEntity.BasketId, basketLineId = basketLineEntity.BasketLineId },
                basketLineToReturn);
        } 

        [HttpPut("{basketLineId}")]
        public async Task<ActionResult<BasketLine>> Put(Guid basketId, 
            Guid basketLineId, 
            [FromBody] BasketLineForUpdate basketLineForUpdate)
        {
            if (!await _basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLineEntity = await _basketLinesRepository.GetBasketLineById(basketLineId);

            if (basketLineEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(basketLineForUpdate, basketLineEntity);

            _basketLinesRepository.UpdateBasketLine(basketLineEntity);
            await _basketLinesRepository.SaveChanges();

            return Ok(_mapper.Map<BasketLine>(basketLineEntity));
        } 

        [HttpDelete("{basketLineId}")]
        public async Task<IActionResult> Delete(Guid basketId, 
            Guid basketLineId)
        {
            if (!await _basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLineEntity = await _basketLinesRepository.GetBasketLineById(basketLineId);

            if (basketLineEntity == null)
            {
                return NotFound();
            }

            var basket = await _basketRepository.GetBasketById(basketId);

            _basketLinesRepository.RemoveBasketLine(basketLineEntity);
            await _basketLinesRepository.SaveChanges();

            //publish removal event
            Entities.BasketChangeEvent basketChangeEvent = new Entities.BasketChangeEvent
            {
                BasketChangeType = Entities.BasketChangeTypeEnum.Remove,
                VehicleId = basketLineEntity.VehicleId,
                OptionPackId = basketLineEntity.OptionPackId,
                EngineId = basketLineEntity.EngineId,
                ChassisId = basketLineEntity.ChassisId,
                InsertedAt = DateTime.Now,
                UserId = basket.UserId
            };
            await _basketChangeEventRepository.AddBasketEvent(basketChangeEvent);

            return NoContent();
        }
    }
}
