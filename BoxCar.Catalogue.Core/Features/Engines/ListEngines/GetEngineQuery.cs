using MediatR;

namespace BoxCar.Catalogue.Core.Features.Engines.ListEngines
{
    public class GetEngineQuery : IRequest<GetEngineQueryResponse>
    {
        private int pageSize = 50;

        public int PageNumber { get; set; }

        public int PageSize { get => pageSize; set => pageSize = value > 50 ? 50 : value; }
    }
}
