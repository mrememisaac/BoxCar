using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using BoxCar.Admin.Core.Features.OptionPacks.ListOptionPacks;
using BoxCar.Shared.Caching;

namespace BoxCar.Admin.Core.Features.Vehicles.ListVehicles
{

    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, GetVehicleQueryResponse>
    {
        private readonly IVehicleRepository _repository;
        private readonly ILogger<GetVehicleQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetVehicleQueryHandler(IVehicleRepository repository,
            ILogger<GetVehicleQueryHandler> logger,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetVehicleQueryResponse> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            var key = $"{nameof(GetVehicleQuery)}-{request.PageNumber}-{request.PageSize}";
            var data = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            var response = _mapper.Map<GetVehicleQueryResponse>(data);
            return await _cache.GetFromCache<GetVehicleQueryResponse>(key) ?? await _cache.SaveToCache(key, response);
        }
    }
}
