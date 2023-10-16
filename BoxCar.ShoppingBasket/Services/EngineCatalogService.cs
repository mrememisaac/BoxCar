using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Extensions;

namespace BoxCar.ShoppingBasket.Services
{
    public class EngineCatalogService : IEngineCatalogService
    {
        private readonly HttpClient client;

        public EngineCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Engine> GetEngine(Guid id)
        {
            var response = await client.GetAsync($"/api/engines/{id}");
            return await response.ReadContentAs<Engine>();
        }
    }
}
