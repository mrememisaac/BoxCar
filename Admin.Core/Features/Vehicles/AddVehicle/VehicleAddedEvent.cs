using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{
    public class VehicleAddedEvent : IntegrationBaseMessage
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ChassisDto Chassis { get; set; }

        public EngineDto Engine { get; set; }

        public OptionPackDto OptionPack { get; set; }

        public int Price { get; set; }

    }
}
