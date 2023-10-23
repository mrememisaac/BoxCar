using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public interface IChassisCatalogService
    {
        Task<ChassisAddedEvent> GetChassis(Guid id);
    }
}