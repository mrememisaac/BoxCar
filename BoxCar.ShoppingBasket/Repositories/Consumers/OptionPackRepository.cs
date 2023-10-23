using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Repositories.Consumers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories.Consumers
{
    public class OptionPackRepository : IOptionPacksRepository
    {
        private readonly DbContextOptions<ShoppingBasketDbContext> _options;

        public OptionPackRepository(DbContextOptions<ShoppingBasketDbContext> dbContextOptions)
        {
            _options = dbContextOptions;
        }

        public async Task<Entities.OptionPack> CreateAsync(Entities.OptionPack entity)
        {
            using var context = new ShoppingBasketDbContext(_options);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Entities.OptionPack?> GetByIdAsync(Guid id)
        {
            using var context = new ShoppingBasketDbContext(_options);
            return await context.OptionPacks.FirstOrDefaultAsync(i => i.Id == id);
        }
    }

}
