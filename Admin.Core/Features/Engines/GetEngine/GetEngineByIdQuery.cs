using MediatR;

namespace BoxCar.Admin.Core.Features.Engines.GetEngine
{
    public class GetEngineByIdQuery : IRequest<GetEngineByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
