using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Extensions;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public class EngineCatalogService : IEngineCatalogService
    {
        private readonly HttpClient client;

        public EngineCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<EngineAddedEvent> GetEngine(Guid id)
        {
            var response = await client.GetAsync($"/api/engines/{id}");
            return await response.ReadContentAs<EngineAddedEvent>();
        }
    }
}
