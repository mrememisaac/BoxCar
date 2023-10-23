using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using BoxCar.Shared.Caching;

namespace BoxCar.Admin.Core.Features.Options.ListOptions
{
    public class GetOptionQueryHandler : IRequestHandler<GetOptionQuery, GetOptionQueryResponse>
    {
        private readonly IAsyncRepository<Option, Guid> _repository;
        private readonly ILogger<GetOptionQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetOptionQueryHandler(IAsyncRepository<Option, Guid> repository,
            ILogger<GetOptionQueryHandler> logger,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetOptionQueryResponse> Handle(GetOptionQuery request, CancellationToken cancellationToken)
        {
            var key = $"{nameof(GetOptionQuery)}-{request.PageNumber}-{request.PageSize}";
            var data = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var response = _mapper.Map<GetOptionQueryResponse>(data);
            return await _cache.GetFromCache<GetOptionQueryResponse>(key) ?? await _cache.SaveToCache(key, response);            
        }
    }
}
