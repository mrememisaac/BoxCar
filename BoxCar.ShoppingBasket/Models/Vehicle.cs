using System;

namespace BoxCar.ShoppingBasket.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Chassis Chassis { get; set; }

        public Engine Engine { get; set; }

        public OptionPack OptionPack { get; set; }

        public int Price { get; set; }
    }
}
