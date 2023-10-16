using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Services
{
    public interface IOptionPackCatalogService
    {
        Task<OptionPack> GetOptionPack(Guid id);
    }
}