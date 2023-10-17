using BoxCar.Integration.Messages;

namespace BoxCar.Ordering.Messages
{
    public class OrderCancellationRequest : IntegrationBaseMessage
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }
    }
}
