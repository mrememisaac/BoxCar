using BoxCar.Integration.Messages;

namespace BoxCar.ShoppingBasket.Messaging.Messages
{
    public class VehicleAddedEvent
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }

        public string Name { get; set; } = null!;

        public ChassisDto Chassis { get; set; }

        public EngineDto Engine { get; set; }

        public OptionPackDto OptionPack { get; set; }

        public int Price { get; set; }

    }
}
