using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using BoxCar.Admin.Core.Features.Engines.ListEngines;
using BoxCar.Shared.Caching;

namespace BoxCar.Admin.Core.Features.Chasis.ListChassis
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
            var key = $"{nameof(GetChassisQuery)}-{request.PageNumber}-{request.PageSize}";
            var data = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var response = _mapper.Map<GetChassisQueryResponse>(data);
            return await _cache.GetFromCache<GetChassisQueryResponse>(key) ?? await _cache.SaveToCache<GetChassisQueryResponse>(key, response);
        }
    }
}
