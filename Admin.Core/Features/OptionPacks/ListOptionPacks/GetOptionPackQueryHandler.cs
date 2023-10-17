using AutoMapper;
using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoxCar.Admin.Core.Features.OptionPacks.ListOptionPacks
{

    public class GetOptionPackQueryHandler : IRequestHandler<GetOptionPackQuery, GetOptionPackQueryResponse>
    {
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<GetOptionPackQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetOptionPackQueryHandler(IAsyncRepository<OptionPack, Guid> repository,
            ILogger<GetOptionPackQueryHandler> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetOptionPackQueryResponse> Handle(GetOptionPackQuery request, CancellationToken cancellationToken)
        {

            var response = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
            return _mapper.Map<GetOptionPackQueryResponse>(response);
        }
    }
}
