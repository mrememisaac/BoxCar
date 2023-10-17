
namespace BoxCar.Admin.Core.Features.Vehicles.GetVehicle
{
    public class GetVehicleByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ChassisDto Chassis { get; set; }

        public EngineDto Engine { get; set; }

        public OptionPackDto OptionPack { get; set; }

        public int Price { get; set; }
    }
}
