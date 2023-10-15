using System;
using System.Threading.Tasks;
using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories
{
    public interface IVehicleRepository
    {
        void AddVehicle(Vehicle theVehicle);
        Task<bool> VehicleExists(Guid idd);
        Task<bool> SaveChanges();
    }
}