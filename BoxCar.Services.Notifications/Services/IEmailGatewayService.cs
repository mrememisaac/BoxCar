using BoxCar.Services.Payment.Models;

namespace BoxCar.Services.Payment.Services
{
    public interface IEmailGatewayService
    {
        Task<bool> SendEmail(EmailInfo email);
    }
}
