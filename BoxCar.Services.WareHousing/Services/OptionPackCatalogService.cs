using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Extensions;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public class OptionPackCatalogService : IOptionPackCatalogService
    {
        private readonly HttpClient client;

        public OptionPackCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<OptionPackAddedEvent> GetOptionPack(Guid id)
        {
            var response = await client.GetAsync($"/api/optionpacks/{id}");
            return await response.ReadContentAs<OptionPackAddedEvent>();
        }
    }
}
