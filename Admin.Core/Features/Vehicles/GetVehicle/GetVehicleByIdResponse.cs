using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Features.Vehicles.GetVehicle
{
    public class GetVehicleByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ChassisDto Chassis { get; set; }

        public EngineDto Engine { get; set; }

        public OptionPackDto OptionPack { get; set; }

    }

    public class OptionPackDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public List<OptionDto> Options = new List<OptionDto>();
    }

    public class OptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;
    }

    public class ChassisDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
    }
    public class EngineDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }

    }
}
