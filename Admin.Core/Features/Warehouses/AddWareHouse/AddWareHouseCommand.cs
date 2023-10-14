using MediatR;

namespace BoxCar.Admin.Core.Features.Warehouses.AddWareHouse
{
    public class AddWareHouseCommand : IRequest<Result<AddWareHouseResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }
    }
}
