using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{
    public class VehicleAddedEvent : IntegrationBaseMessage
    {
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
