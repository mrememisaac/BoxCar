using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Extensions;

namespace BoxCar.ShoppingBasket.Services
{
    public class OptionPackCatalogService : IOptionPackCatalogService
    {
        private readonly HttpClient client;

        public OptionPackCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<OptionPack> GetOptionPack(Guid id)
        {
            var response = await client.GetAsync($"/api/optionpacks/{id}");
            return await response.ReadContentAs<OptionPack>();
        }
    }
}
