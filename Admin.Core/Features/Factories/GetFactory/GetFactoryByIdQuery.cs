using MediatR;

namespace BoxCar.Admin.Core.Features.Factories.GetFactory
{
    public class GetFactoryByIdQuery : IRequest<GetFactoryByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
