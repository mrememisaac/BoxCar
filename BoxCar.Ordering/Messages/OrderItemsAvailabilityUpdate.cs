using BoxCar.Integration.Messages;

namespace BoxCar.Ordering.Messages
{
    public class OrderItemsAvailabilityUpdate : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

        public List<OrderItemAvailabilityLine> Lines { get; set; } = new();

        public bool ReadyForPickup { get; set; }
    }
}
