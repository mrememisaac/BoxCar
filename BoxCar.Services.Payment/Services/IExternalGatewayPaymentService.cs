using BoxCar.Integration.MessageBus;
using BoxCar.Services.Payment.Messages;
using BoxCar.Services.Payment.Models;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace BoxCar.Services.Payment.Services
{
    public interface IExternalGatewayPaymentService
    {
        Task<bool> PerformPayment(PaymentInfo paymentInfo);
    }
}
