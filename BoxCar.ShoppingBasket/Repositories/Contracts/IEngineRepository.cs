using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories.Contracts
{
    public interface IEngineRepository
    {
        void AddEngine(Engine engine);
        Task<bool> EngineExists(Guid id);
        Task<bool> SaveChanges();
    }
}