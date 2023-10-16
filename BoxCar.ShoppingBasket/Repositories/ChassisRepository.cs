using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories
{
    public class ChassisRepository : IChassisRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public ChassisRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            _shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<bool> ChassisExists(Guid id)
        {
            return await _shoppingBasketDbContext.Chassis.AnyAsync(e => e.Id == id);
        }

        public void AddChassis(Chassis theChassis)
        {
            _shoppingBasketDbContext.Chassis.Add(theChassis);

        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
