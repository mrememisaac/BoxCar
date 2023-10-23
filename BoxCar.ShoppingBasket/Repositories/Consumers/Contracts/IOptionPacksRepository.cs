namespace BoxCar.ShoppingBasket.Repositories.Consumers.Contracts
{
    public interface IOptionPacksRepository
    {
        Task<Entities.OptionPack> GetByIdAsync(Guid id);
        Task<Entities.OptionPack?> CreateAsync(Entities.OptionPack OptionPack);
    }

}
