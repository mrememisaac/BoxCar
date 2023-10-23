using System;

namespace BoxCar.ShoppingBasket.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual Chassis? Chassis { get; set; }

        public virtual Engine? Engine { get; set; }

        public virtual OptionPack? OptionPack { get; set; }

        public int Price { get; set; }

        public Guid ChassisId { get; set; }
        public Guid EngineId { get; set; }
        public Guid OptionPackId { get; set; }
    }
}
