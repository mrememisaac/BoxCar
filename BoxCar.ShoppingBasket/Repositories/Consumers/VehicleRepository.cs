using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Repositories.Consumers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories.Consumers
{
    public class VehicleRepository : IVehiclesRepository
    {
        private readonly DbContextOptions<ShoppingBasketDbContext> _options;

        public VehicleRepository(DbContextOptions<ShoppingBasketDbContext> dbContextOptions)
        {
            _options = dbContextOptions;
        }

        public async Task<Entities.Vehicle> CreateAsync(Entities.Vehicle entity)
        {
            using var context = new ShoppingBasketDbContext(_options);
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Entities.Vehicle?> GetByIdAsync(Guid id)
        {
            using var context = new ShoppingBasketDbContext(_options);
            return await context.Vehicles.FirstOrDefaultAsync(i => i.Id == id);
        }
    }


}
