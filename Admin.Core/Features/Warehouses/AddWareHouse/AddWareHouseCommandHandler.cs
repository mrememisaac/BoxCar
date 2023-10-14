using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Core.Features.Warehouses.AddWareHouse
{
    public class AddWareHouseCommandHandler : IRequestHandler<AddWareHouseCommand, Result<AddWareHouseResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<WareHouse, Guid> _repository;
        private readonly ILogger<AddWareHouseCommandHandler> _logger;
        private readonly AddWareHouseCommandValidator _validator;

        public AddWareHouseCommandHandler(IMapper mapper,
            IAsyncRepository<WareHouse, Guid> repository,
            ILogger<AddWareHouseCommandHandler> logger,
            AddWareHouseCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<AddWareHouseResponse>> Handle(AddWareHouseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a WareHouse {request}", request);
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var wareHouse = _mapper.Map<WareHouse>(request);
            await _repository.CreateAsync(wareHouse, cancellationToken);
            return new Result<AddWareHouseResponse>(_mapper.Map<AddWareHouseResponse>(wareHouse));
        }
    }
}
