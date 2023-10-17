using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoxCar.Admin.Core.Features.Vehicles.ListVehicles
{

    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, GetVehicleQueryResponse>
    {
        private readonly IAsyncRepository<Vehicle, Guid> _repository;
        private readonly ILogger<GetVehicleQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetVehicleQueryHandler(IAsyncRepository<Vehicle, Guid> repository,
            ILogger<GetVehicleQueryHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetVehicleQueryResponse> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {

            var response = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            return _mapper.Map<GetVehicleQueryResponse>(response);
        }
    }
}
