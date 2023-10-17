using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoxCar.Admin.Core.Features.Chasis.ListChassis
{

    public class GetChassisQueryHandler : IRequestHandler<GetChassisQuery, GetChassisQueryResponse>
    {
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<GetChassisQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetChassisQueryHandler(IAsyncRepository<Chassis, Guid> repository,
            ILogger<GetChassisQueryHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetChassisQueryResponse> Handle(GetChassisQuery request, CancellationToken cancellationToken)
        {

            var response = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            return _mapper.Map<GetChassisQueryResponse>(response);
        }
    }
}
