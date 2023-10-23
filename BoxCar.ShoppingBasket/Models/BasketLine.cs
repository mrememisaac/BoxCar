using System;

namespace BoxCar.ShoppingBasket.Models
{
    public class BasketLine
    {
        public Guid BasketLineId { get; set; }
        public Guid BasketId { get; set; }
        public Guid VehicleId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
} 
