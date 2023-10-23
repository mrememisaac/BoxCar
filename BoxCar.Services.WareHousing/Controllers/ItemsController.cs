using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Services.WareHousing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpPost("{id}/deplete")]
        public async Task<ActionResult<Item>> Deplete(Guid id, int quantity = 5)
        {
            return await ChangeQuantity(id, quantity*-1);
        }

        [HttpPost("{id}/restock")]
        public async Task<ActionResult<Item>> Restock(Guid id, int quantity = 5)
        {
            return await ChangeQuantity(id, quantity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(Guid id)
        {
            var item = await _itemsRepository.GetByItemTypeId(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
        private async Task<ActionResult<Item>> ChangeQuantity(Guid id, int quantity = 5)
        {
            var item = await _itemsRepository.GetByItemTypeId(id);
            if (item == null) return NotFound();
            await _itemsRepository.ChangeStockCount(item.SpecificationKey, quantity);
            item = await _itemsRepository.GetByItemTypeId(id);
            return Ok(item);
        }

    }
}
