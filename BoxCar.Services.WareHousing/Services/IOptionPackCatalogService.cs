using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public interface IOptionPackCatalogService
    {
        Task<OptionPackAddedEvent> GetOptionPack(Guid id);
    }
}