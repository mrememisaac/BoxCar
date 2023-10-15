using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Features.Engines.AddEngine
{
    public class AddEngineDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }
        public int Price { get; set; }
    }
}
