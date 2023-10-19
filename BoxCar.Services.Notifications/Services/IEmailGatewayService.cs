using BoxCar.Services.Payment.Models;

namespace BoxCar.Services.Notifications.Services
{
    public interface IEmailGatewayService
    {
        Task<bool> SendEmail(EmailInfo email);
    }
}
