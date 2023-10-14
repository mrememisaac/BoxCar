using MediatR;

namespace BoxCar.Admin.Core.Features.Chasis.GetChassis
{
    public class GetChassisByIdQuery : IRequest<GetChassisByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
