using MediatR;

namespace Admin.Core.Features.Warehouses.GetWareHouse
{
    public class GetWareHouseByIdQuery : IRequest<GetWareHouseByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
