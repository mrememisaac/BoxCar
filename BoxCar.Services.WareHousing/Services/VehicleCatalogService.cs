using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Extensions;
using BoxCar.Services.WareHousing.Messages;

namespace BoxCar.Services.WareHousing.Services
{
    public class VehicleCatalogService : IVehicleCatalogService
    {
        private readonly HttpClient client;

        public VehicleCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<VehicleAddedEvent> GetVehicle(Guid id)
        {
            var response = await client.GetAsync($"/api/vehicles/{id}");
            return await response.ReadContentAs<VehicleAddedEvent>();
        }
    }
}
