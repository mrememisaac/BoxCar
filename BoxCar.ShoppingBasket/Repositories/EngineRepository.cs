using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories
{
    public class EngineRepository : IEngineRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public EngineRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            _shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<bool> EngineExists(Guid id)
        {
            return await _shoppingBasketDbContext.Engines.AnyAsync(e => e.Id == id);
        }

        public void AddEngine(Engine theEngine)
        {
            _shoppingBasketDbContext.Engines.Add(theEngine);

        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
