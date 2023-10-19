using AutoMapper;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Core.Features.Chasis.GetChassis;
using BoxCar.Catalogue.Domain;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using BoxCar.Shared.Caching;
using BoxCar.Catalogue.Core.Features.Engines.ListEngines;

namespace BoxCar.Catalogue.Core.Features.Chasis.ListChassis
{

    public class GetChassisQueryHandler : IRequestHandler<GetChassisQuery, GetChassisQueryResponse>
    {
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<GetChassisQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public GetChassisQueryHandler(IAsyncRepository<Chassis, Guid> repository,
            ILogger<GetChassisQueryHandler> logger,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetChassisQueryResponse> Handle(GetChassisQuery request, CancellationToken cancellationToken)
        {
            var key = $"{nameof(GetEngineQuery)}-{request.PageNumber}-{request.PageSize}";
            var response = await _cache.GetFromCache<IEnumerable<Chassis>>(key) ?? await _cache.SaveToCache<IEnumerable<Chassis>>(key,
               await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken)
               );
            return _mapper.Map<GetChassisQueryResponse>(response);
        }
    }
}
