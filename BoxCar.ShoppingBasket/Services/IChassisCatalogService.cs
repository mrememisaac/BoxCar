using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Services
{
    public interface IChassisCatalogService
    {
        Task<Chassis> GetChassis(Guid id);
    }
}