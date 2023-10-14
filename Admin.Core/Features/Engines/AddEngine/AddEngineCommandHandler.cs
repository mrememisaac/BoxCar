using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoxCar.Admin.Core.Features.Engines.AddEngine
{
    public class AddEngineCommandHandler : IRequestHandler<AddEngineCommand, Result<AddEngineResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Engine, Guid> _repository;
        private readonly ILogger<AddEngineCommandHandler> _logger;
        private readonly AddEngineCommandValidator _validator;

        public AddEngineCommandHandler(IMapper mapper,
            IAsyncRepository<Engine, Guid> repository,
            ILogger<AddEngineCommandHandler> logger,
            AddEngineCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<AddEngineResponse>> Handle(AddEngineCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding an Engine {request}", request);
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var engine = _mapper.Map<Engine>(request);
            await _repository.CreateAsync(engine, cancellationToken);
            return new Result<AddEngineResponse>(_mapper.Map<AddEngineResponse>(engine));
        }
    }
}
