using BoxCar.Services.WareHousing.Entities;

namespace BoxCar.Services.WareHousing.Repositories
{
    public interface IItemsRepository
    {
        Task Add(Item item);

        Task Add(IEnumerable<Item> items);

        Task<Item> GetById(Guid id);

    }
}
