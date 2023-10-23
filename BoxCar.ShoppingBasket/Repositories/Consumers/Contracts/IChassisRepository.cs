using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories.Consumers.Contracts
{
    public interface IChassisRepository
    {
        Task<Chassis> GetByIdAsync(Guid id);
        Task<Chassis?> CreateAsync(Chassis chassis);
    }
}

