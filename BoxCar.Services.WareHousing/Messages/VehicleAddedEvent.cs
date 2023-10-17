using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class VehicleAddedEvent : IntegrationBaseMessage
    {
        public Guid VehicleId { get; set; }

        public string Name { get; set; } = null!;

    }
}
