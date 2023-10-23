namespace BoxCar.ShoppingBasket.Repositories.Consumers.Contracts
{
    public interface IVehiclesRepository
    {
        Task<Entities.Vehicle> GetByIdAsync(Guid id);
        Task<Entities.Vehicle?> CreateAsync(Entities.Vehicle Vehicle);
    }


}
