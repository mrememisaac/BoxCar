using BoxCar.Integration.Messages;

namespace BoxCar.Ordering.Messages
{
    public class FulfillOrderRequest : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }

        public List<FulfillOrderRequestLine> OrderItems { get; set; } = new();
    }
}
