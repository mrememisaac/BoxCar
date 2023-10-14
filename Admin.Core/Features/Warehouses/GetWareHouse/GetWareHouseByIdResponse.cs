using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Features.Warehouses.GetWareHouse
{
    public class GetWareHouseByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

    }
}
