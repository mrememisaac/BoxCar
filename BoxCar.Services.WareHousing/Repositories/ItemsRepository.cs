using BoxCar.Services.WareHousing.DbContexts;
using BoxCar.Services.WareHousing.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Services.WareHousing.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DbContextOptions<ItemsDbContext> dbContextOptions;

        public ItemsRepository(DbContextOptions<ItemsDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task Add(Item item)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            _dbContext.Items.Add(item);
            _dbContext.SaveChangesAsync();
        }

        public async Task Add(IEnumerable<Item> items)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            _dbContext.Items.AddRange(items);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Item> GetById(Guid id)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.FindAsync(id);
        }
    }
}
