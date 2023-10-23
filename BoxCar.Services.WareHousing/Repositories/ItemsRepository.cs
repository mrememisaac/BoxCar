using BoxCar.Services.WareHousing.DbContexts;
using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BoxCar.Services.WareHousing.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DbContextOptions<ItemsDbContext> dbContextOptions;

        public ItemsRepository(DbContextOptions<ItemsDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<Item> Add(Item item)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
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
            var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.ItemTypeId == itemTypeId && i.ItemType == type);
            return item;
        }

        public async Task<Item?> GetByItemTypeId(Guid id)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.FirstOrDefaultAsync(i => i.ItemTypeId == id);
        }

        public async Task<Item?> GetBySpecificationKey(string specification)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            var it = await _dbContext.Items.FirstOrDefaultAsync(i => i.SpecificationKey.Equals(specification));
            return it;
        }

        public async Task<IEnumerable<Item>> GetComponents(FulfillOrderRequestLine line)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            return await _dbContext.Items.Where(x => x.ItemTypeId == line.VehicleId || 
                        x.ItemTypeId == line.EngineId || 
                        x.ItemTypeId == line.ChassisId || 
                        x.ItemTypeId == line.OptionPackId).ToListAsync();
        }

        public async Task<int> ReduceStockCount(string specification, int quantity) => await ChangeStockCount(specification, -1 * quantity);

        public async Task<int> IncreaseStockCount(string specification, int quantity) => await ChangeStockCount(specification, quantity);

        public async Task<int> ChangeStockCount(string specification, int quantity)
        {
            await using var _dbContext = new ItemsDbContext(dbContextOptions);
            var vehicleWithMatchingSpecification = await _dbContext.Items.FirstOrDefaultAsync(i => i.SpecificationKey.Equals(specification, StringComparison.OrdinalIgnoreCase));
            if (vehicleWithMatchingSpecification != null)
            {
                vehicleWithMatchingSpecification.Quantity += quantity;
                _dbContext.SaveChanges();
                return vehicleWithMatchingSpecification.Quantity;
            }
            return 0;
        }
    }
}
