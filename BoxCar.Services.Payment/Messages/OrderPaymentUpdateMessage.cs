using System;
using BoxCar.Integration.Messages;

namespace BoxCar.Services.Payment.Messages
{
    public class OrderPaymentUpdateMessage: IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentSuccess { get; set; }
    }
}
