using BoxCar.ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace BoxCar.ShoppingBasket.Repositories.Contracts
{
    public interface IBasketRepository
    {
        Task<bool> BasketExists(Guid basketId);

        Task<Basket> GetBasketById(Guid basketId);

        void AddBasket(Basket basket);

        Task<bool> SaveChanges();

        Task ClearBasket(Guid basketId);
    }
}