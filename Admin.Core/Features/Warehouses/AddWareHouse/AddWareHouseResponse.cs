namespace BoxCar.Admin.Core.Features.Warehouses.AddWareHouse
{
    public class AddWareHouseResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }
    }
}
