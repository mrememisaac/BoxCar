using System;
using System.ComponentModel.DataAnnotations;

namespace BoxCar.ShoppingBasket.Models
{
    public class BasketLineForCreation
    { 
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
