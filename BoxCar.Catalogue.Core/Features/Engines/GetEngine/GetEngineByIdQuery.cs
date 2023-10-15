using MediatR;

namespace BoxCar.Catalogue.Core.Features.Engines.GetEngine
{
    public class GetEngineByIdQuery : IRequest<GetEngineByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
