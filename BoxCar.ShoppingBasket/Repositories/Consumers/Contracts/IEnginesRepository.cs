namespace BoxCar.ShoppingBasket.Repositories.Consumers.Contracts
{
    public interface IEnginesRepository
    {
        Task<Entities.Engine> GetByIdAsync(Guid id);
        Task<Entities.Engine?> CreateAsync(Entities.Engine Engine);
    }
}
