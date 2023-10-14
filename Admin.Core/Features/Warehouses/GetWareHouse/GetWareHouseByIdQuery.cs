using MediatR;

namespace BoxCar.Admin.Core.Features.Warehouses.GetWareHouse
{
    public class GetWareHouseByIdQuery : IRequest<GetWareHouseByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
