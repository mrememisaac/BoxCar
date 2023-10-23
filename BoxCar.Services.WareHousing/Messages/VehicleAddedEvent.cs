using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class VehicleAddedEvent
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }

        public string Name { get; set; } = null!;
        
        public ChassisAddedEvent Chassis { get; set; }

        public EngineAddedEvent Engine { get; set; }

        public OptionPackAddedEvent OptionPack { get; set; }

        public int Price { get; set; }

        public Guid ChassisId { get; set; }
        public Guid EngineId { get; set; }
        public Guid OptionPackId { get; set; }

    }
}
