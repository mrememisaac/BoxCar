using MediatR;

namespace BoxCar.Admin.Core.Features.Vehicles.GetVehicle
{
    public class GetVehicleByIdQuery : IRequest<GetVehicleByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
