using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Extensions;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public class ChassisCatalogService : IChassisCatalogService
    {
        private readonly HttpClient client;

        public ChassisCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ChassisAddedEvent> GetChassis(Guid id)
        {
            var response = await client.GetAsync($"/api/chassis/{id}");
            return await response.ReadContentAs<ChassisAddedEvent>();
        }
    }
}
