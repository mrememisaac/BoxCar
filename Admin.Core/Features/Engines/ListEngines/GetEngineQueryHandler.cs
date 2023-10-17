using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoxCar.Admin.Core.Features.Engines.ListEngines
{

    public class GetEngineQueryHandler : IRequestHandler<GetEngineQuery, GetEngineQueryResponse>
    {
        private readonly IAsyncRepository<Engine, Guid> _repository;
        private readonly ILogger<GetEngineQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetEngineQueryHandler(IAsyncRepository<Engine, Guid> repository,
            ILogger<GetEngineQueryHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetEngineQueryResponse> Handle(GetEngineQuery request, CancellationToken cancellationToken)
        {

            var response = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            return _mapper.Map<GetEngineQueryResponse>(response);
        }
    }
}
