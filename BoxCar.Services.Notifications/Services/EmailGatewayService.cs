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
            var dataAsString = JsonSerializer.Serialize(emailInfo);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(configuration.GetValue<string>("ApiConfigs:ExternalPaymentGateway:Uri") + "/api/paymentapprover", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<bool>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }
    }
}
