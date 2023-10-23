using System.Net.Http.Headers;
using System.Text.Json;
using BoxCar.Services.Payment.Models;

namespace BoxCar.Services.Notifications.Services
{
    public class EmailGatewayService : IEmailGatewayService
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;


        public EmailGatewayService(HttpClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }

        public async Task<bool> SendEmail(EmailInfo emailInfo)
        {
            return true;

        }
    }
}
