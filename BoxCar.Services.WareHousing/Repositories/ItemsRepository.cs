using BoxCar.Services.WareHousing.DbContexts;
using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;
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

        public async Task<Item?> GetById(Guid id)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.FindAsync(id);
        }

        public async Task<Item?> GetByItemTypeAndItemTypeId(ItemType type, Guid itemTypeId)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.FirstOrDefaultAsync(i => i.ItemTypeId == itemTypeId && i.ItemType == type);
        }

        public async Task<Item?> GetBySpecificationKey(string specification)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.FirstOrDefaultAsync(i => i.SpecificationKey.Equals(specification, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Item>> GetComponents(FulfillOrderRequestLine line)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.Where(x => x.ItemTypeId == line.VehicleId || 
                        x.ItemTypeId == line.EngineId || 
                        x.ItemTypeId == line.ChassisId || 
                        x.ItemTypeId == line.OptionPackId).ToListAsync();
        }

        public async Task ReduceVehicleStockCount(string specification, int reduceByQuantity)
        {
            if (reduceByQuantity <= 0) return;
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            var vehicleWithMatchingSpecification = await _dbContext.Items.FirstOrDefaultAsync(i => i.SpecificationKey.Equals(specification, StringComparison.OrdinalIgnoreCase));
            if (vehicleWithMatchingSpecification != null)
            {
                vehicleWithMatchingSpecification.Quantity -= reduceByQuantity;
                _dbContext.SaveChanges();
            }
        }
    }
}
