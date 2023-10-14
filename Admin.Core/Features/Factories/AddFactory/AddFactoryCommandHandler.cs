using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Core.Features.Factories.AddFactory
{
    public class AddFactoryCommandHandler : IRequestHandler<AddFactoryCommand, Result<AddFactoryCommand>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Factory, Guid> _repository;
        private readonly ILogger<AddFactoryCommandHandler> _logger;
        private readonly AddFactoryCommandValidator _validator;

        public AddFactoryCommandHandler(IMapper mapper,
            IAsyncRepository<Factory, Guid> repository,
            ILogger<AddFactoryCommandHandler> logger,
            AddFactoryCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<AddFactoryCommand>> Handle(AddFactoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a Factory {request}", request);
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var factory = _mapper.Map<Factory>(request);
            await _repository.CreateAsync(factory);
            return new Result<AddFactoryCommand>(true);
        }
    }
}
