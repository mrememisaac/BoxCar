using System;
using System.Threading.Tasks;
using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public interface IVehicleCatalogService
    {
        Task<VehicleAddedEvent> GetVehicle(Guid id);
    }
}