namespace BoxCar.Catalogue.Core.Features.Vehicles.ListVehicles
{
    public class VehicleQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ChassisQueryItem Chassis { get; set; }

        public EngineQueryItem Engine { get; set; }

        public OptionPackQueryItem OptionPack { get; set; }

        public int Price { get; set; }
    }
}
