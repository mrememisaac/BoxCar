using System;
using System.ComponentModel.DataAnnotations;

namespace BoxCar.ShoppingBasket.Models
{
    public class BasketForCreation
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
