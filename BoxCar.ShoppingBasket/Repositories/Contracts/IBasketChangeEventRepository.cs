using BoxCar.ShoppingBasket.Entities;

namespace BoxCar.ShoppingBasket.Repositories.Contracts
{
    public interface IBasketChangeEventRepository
    {
        Task AddBasketEvent(BasketChangeEvent basketChangeEvent);
        Task<List<BasketChangeEvent>> GetBasketChangeEvents(DateTime startDate, int max);
    }
}
