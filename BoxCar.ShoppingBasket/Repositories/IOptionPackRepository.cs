using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories
{
    public interface IOptionPackRepository
    {
        void AddOptionPack(OptionPack optionPack);
        Task<bool> OptionPackExists(Guid id);
        Task<bool> SaveChanges();
    }
}