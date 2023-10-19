using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Core.Features.Engines.ListEngines
{
    public class EngineQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }
    }
}
