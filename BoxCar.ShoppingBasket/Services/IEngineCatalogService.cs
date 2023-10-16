using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Services
{
    public interface IEngineCatalogService
    {
        Task<Engine> GetEngine(Guid id);
    }
}