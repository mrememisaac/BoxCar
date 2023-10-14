using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Core.Features.Factories.AddWareHouse
{
    public class AddWareHouseCommandHandler : IRequestHandler<AddWareHouseCommand, Result<AddWareHouseCommand>>
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
        public async Task<Result<AddWareHouseCommand>> Handle(AddWareHouseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a WareHouse {request}", request);
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var wareHouse = _mapper.Map<WareHouse>(request);
            await _repository.CreateAsync(wareHouse);
            return new Result<AddWareHouseCommand>(true);
        }
    }
}
