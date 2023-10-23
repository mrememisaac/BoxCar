using BoxCar.Integration.Messages;

namespace BoxCar.Catalogue.Messages
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

        public Guid ChassisId { get; set; }
        public Guid EngineId { get; set; }
        public Guid OptionPackId { get; set; }
    }
}
