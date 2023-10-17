using BoxCar.Integration.Messages;

namespace BoxCar.Services.Notifications.Messages
{
    public class OrderStatusUpdateMessage : IntegrationBaseMessage
    {
        public string Email { get; set; }

        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}
