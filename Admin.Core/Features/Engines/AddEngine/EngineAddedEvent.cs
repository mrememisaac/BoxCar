using BoxCar.Admin.Domain;
using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.Engines.AddEngine
{
    public class EngineAddedEvent : IntegrationBaseMessage
    {
        public Guid EngineId { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }
        public int Price { get; set; }
    }
}
