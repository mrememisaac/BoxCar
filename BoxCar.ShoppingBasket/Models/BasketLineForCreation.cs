using System;
using System.ComponentModel.DataAnnotations;

namespace BoxCar.ShoppingBasket.Models
{
    public class BasketLineForCreation
    { 
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid EngineId { get; set; }

        [Required]
        public Guid ChassisId { get; set; }

        [Required]
        public Guid OptionPackId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
