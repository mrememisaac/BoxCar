using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using BoxCar.Shared.Caching;

namespace BoxCar.Admin.Core.Features.OptionPacks.ListOptionPacks
{
    public class GetOptionPackQueryHandler : IRequestHandler<GetOptionPackQuery, GetOptionPackQueryResponse>
    {
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<GetOptionPackQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetOptionPackQueryHandler(IAsyncRepository<OptionPack, Guid> repository,
            ILogger<GetOptionPackQueryHandler> logger,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetOptionPackQueryResponse> Handle(GetOptionPackQuery request, CancellationToken cancellationToken)
        {
            var key = $"{nameof(GetOptionPackQuery)}-{request.PageNumber}-{request.PageSize}";
            var response = await _cache.GetFromCache<IEnumerable<OptionPack>>(key) ?? await _cache.SaveToCache<IEnumerable<OptionPack>>(key,
                await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken)
                );
            return _mapper.Map<GetOptionPackQueryResponse>(response);
        }
    }
}
