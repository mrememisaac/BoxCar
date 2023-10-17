using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class OrderItemsAvailabilityUpdate : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }

        public List<OrderItemAvailabilityLine> Lines { get; set; } = new();
    }
}
