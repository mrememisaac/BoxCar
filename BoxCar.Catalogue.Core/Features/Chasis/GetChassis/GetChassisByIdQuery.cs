using MediatR;

namespace BoxCar.Catalogue.Core.Features.Chasis.GetChassis
{
    public class GetChassisByIdQuery : IRequest<GetChassisByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
