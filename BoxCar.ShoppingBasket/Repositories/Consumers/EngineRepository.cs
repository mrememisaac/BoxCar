using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Repositories.Consumers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories.Consumers
{

    public class EngineRepository : IEnginesRepository
    {
        private readonly DbContextOptions<ShoppingBasketDbContext> _options;

        public EngineRepository(DbContextOptions<ShoppingBasketDbContext> dbContextOptions)
        {
            _options = dbContextOptions;
        }

        public async Task<Entities.Engine> CreateAsync(Entities.Engine entity)
        {
            using var context = new ShoppingBasketDbContext(_options);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Entities.Engine?> GetByIdAsync(Guid id)
        {
            using var context = new ShoppingBasketDbContext(_options);
            return await context.Engines.FirstOrDefaultAsync(i => i.Id == id);
        }
    }


}
