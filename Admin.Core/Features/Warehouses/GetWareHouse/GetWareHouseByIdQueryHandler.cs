using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Core.Features.Warehouses.GetWareHouse
{
    public class GetWareHouseByIdQueryHandler : IRequestHandler<GetWareHouseByIdQuery, GetWareHouseByIdResponse>
    {
        private readonly IAsyncRepository<WareHouse, Guid> _repository;
        private readonly ILogger<GetWareHouseByIdQueryHandler> _logger;
        private readonly GetWareHouseByIdQueryValidator _validator;
        private readonly IMapper _mapper;

        public GetWareHouseByIdQueryHandler(IAsyncRepository<WareHouse, Guid> repository, 
            ILogger<GetWareHouseByIdQueryHandler> logger, 
            GetWareHouseByIdQueryValidator validator,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetWareHouseByIdResponse> Handle(GetWareHouseByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var response = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetWareHouseByIdResponse>(response);
        }
    }
}
