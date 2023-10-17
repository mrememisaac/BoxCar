using AutoMapper;
using BoxCar.ShoppingBasket.Messages;
using BoxCar.ShoppingBasket.Models;

namespace BoxCar.ShoppingBasket.Profiles
{
    public class BasketCheckoutProfile : Profile
    {
        public BasketCheckoutProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutMessage>().ReverseMap();
        }
    }
}
