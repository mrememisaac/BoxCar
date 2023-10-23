using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using BoxCar.Integration.MessageBus;
using Microsoft.Extensions.Configuration;

namespace BoxCar.Admin.Core.Features.Options.AddOption
{

    public class AddOptionCommandHandler : IRequestHandler<AddOptionCommand, Result<AddOptionResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Option, Guid> _repository;
        private readonly ILogger<AddOptionCommandHandler> _logger;
        private readonly AddOptionCommandValidator _validator;
        private readonly IMessageBus _messageBus;
        private string _eventTopic;

        public AddOptionCommandHandler(IMapper mapper,
            IAsyncRepository<Option, Guid> repository,
            ILogger<AddOptionCommandHandler> logger,
            AddOptionCommandValidator validator, IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _messageBus = messageBus;
            _eventTopic = configuration.GetValue<string>(nameof(OptionAddedEvent)) ?? throw new ArgumentNullException($"{nameof(OptionAddedEvent)} configuration value missing");
        }
        public async Task<Result<AddOptionResponse>> Handle(AddOptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding an Engine {request}", request);
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var option = _mapper.Map<Option>(request);
            option = await _repository.CreateAsync(option, cancellationToken);
            var newEvent = _mapper.Map<OptionAddedEvent>(option);
            try
            {
                await _messageBus.PublishMessage(newEvent, _eventTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Writing {newEvent} to message bus failed. Rolling back", newEvent);
                await _repository.DeleteAsync(option, cancellationToken);
                throw;
            }
            return new Result<AddOptionResponse>(_mapper.Map<AddOptionResponse>(option));
        }
    }
}
