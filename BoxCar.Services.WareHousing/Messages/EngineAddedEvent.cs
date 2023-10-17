using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class EngineAddedEvent
    {
        public Guid EngineId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }

        public int Price { get; set; }
    }
}
