using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Repositories
{
    public interface IItemsRepository
    {
        Task Add(Item item);

        Task Add(IEnumerable<Item> items);

        Task<Item?> GetById(Guid id);

        Task<Item?> GetByItemTypeAndItemTypeId(ItemType type, Guid itemTypeId);

        Task<Item?> GetBySpecificationKey(string specification);
        
        Task<IEnumerable<Item>> GetComponents(FulfillOrderRequestLine line);
        
        Task ReduceVehicleStockCount(string specification, int reduceByQuantity);
    }
}
