using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using BoxCar.Shared.Caching;

namespace BoxCar.Admin.Core.Features.Engines.ListEngines
{

    public class GetEngineQueryHandler : IRequestHandler<GetEngineQuery, GetEngineQueryResponse>
    {
        private readonly IAsyncRepository<Engine, Guid> _repository;
        private readonly ILogger<GetEngineQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetEngineQueryHandler(IAsyncRepository<Engine, Guid> repository,
            ILogger<GetEngineQueryHandler> logger,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetEngineQueryResponse> Handle(GetEngineQuery request, CancellationToken cancellationToken)
        {
            var key = $"{nameof(GetEngineQuery)}-{request.PageNumber}-{request.PageSize}";
            var response = await _cache.GetFromCache<IEnumerable<Engine>>(key) ?? await _cache.SaveToCache<IEnumerable<Engine>>(key,
                await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken)
                );
            return _mapper.Map<GetEngineQueryResponse>(response);
        }
    }
}
