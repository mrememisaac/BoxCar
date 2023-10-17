using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class EngineAddedEvent : IntegrationBaseMessage
    {
        public Guid EngineId { get; set; }

        public string Name { get; set; } = null!;
    }
}
