

using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Messages
{
    public class EngineDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }

        public int Price { get; set; }
    }
}
