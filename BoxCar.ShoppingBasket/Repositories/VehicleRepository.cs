using System;
using System.Threading.Tasks;
using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.ShoppingBasket.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public VehicleRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            _shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<bool> VehicleExists(Guid id)
        {
            return await _shoppingBasketDbContext.Vehicles.AnyAsync(e => e.Id == id);
        }

        public void AddVehicle(Vehicle theVehicle)
        {
            _shoppingBasketDbContext.Vehicles.Add(theVehicle);

        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
