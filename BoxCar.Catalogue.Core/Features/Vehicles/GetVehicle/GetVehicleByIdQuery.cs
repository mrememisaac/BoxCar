using MediatR;

namespace BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle
{
    public class GetVehicleByIdQuery : IRequest<GetVehicleByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
