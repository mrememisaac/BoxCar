using System;
using System.Threading.Tasks;
using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Services
{
    public interface IVehicleCatalogService
    {
        Task<Vehicle> GetVehicle(Guid id);
    }
}