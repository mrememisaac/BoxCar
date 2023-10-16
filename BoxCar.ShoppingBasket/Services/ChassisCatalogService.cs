using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Extensions;

namespace BoxCar.ShoppingBasket.Services
{
    public class ChassisCatalogService : IChassisCatalogService
    {
        private readonly HttpClient client;

        public ChassisCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Chassis> GetChassis(Guid id)
        {
            var response = await client.GetAsync($"/api/chassis/{id}");
            return await response.ReadContentAs<Chassis>();
        }
    }
}
