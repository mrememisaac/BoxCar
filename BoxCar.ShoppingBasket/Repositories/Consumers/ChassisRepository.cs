using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Repositories.Consumers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories.Consumers
{
    public class ChassisRepository : IChassisRepository
    {
        private readonly DbContextOptions<ShoppingBasketDbContext> _options;

        public ChassisRepository(DbContextOptions<ShoppingBasketDbContext> dbContextOptions)
        {
            _options = dbContextOptions;
        }

        public async Task<Chassis> CreateAsync(Chassis entity)
        {
            using var context = new ShoppingBasketDbContext(_options);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Chassis?> GetByIdAsync(Guid id)
        {
            using var context = new ShoppingBasketDbContext(_options);
            return await context.Chassis.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
