using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoxCar.ShoppingBasket.Entities;
using BoxCar.ShoppingBasket.Extensions;

namespace BoxCar.ShoppingBasket.Services
{
    public class VehicleCatalogService : IVehicleCatalogService
    {
        private readonly HttpClient client;

        public VehicleCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Vehicle> GetVehicle(Guid id)
        {
            var response = await client.GetAsync($"/api/vehicles/{id}");
            return await response.ReadContentAs<Vehicle>();
        }
    }
}
