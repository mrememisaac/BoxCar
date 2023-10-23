using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories
{
    public class OptionPackRepository : IOptionPackRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public OptionPackRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            _shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<bool> OptionPackExists(Guid id)
        {
            return await _shoppingBasketDbContext.OptionPacks.AnyAsync(e => e.Id == id);
        }

        public void AddOptionPack(OptionPack theOptionPack)
        {
            _shoppingBasketDbContext.OptionPacks.Add(theOptionPack);

        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
