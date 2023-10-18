﻿using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using BoxCar.Integration.MessageBus;
using Microsoft.Extensions.Configuration;

namespace BoxCar.Admin.Core.Features.Engines.AddEngine
{
    public class AddEngineCommandHandler : IRequestHandler<AddEngineCommand, Result<AddEngineResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Engine, Guid> _repository;
        private readonly ILogger<AddEngineCommandHandler> _logger;
        private readonly AddEngineCommandValidator _validator;
        private readonly IMessageBus _messageBus;
        private string _eventTopic;

        public AddEngineCommandHandler(IMapper mapper,
            IAsyncRepository<Engine, Guid> repository,
            ILogger<AddEngineCommandHandler> logger,
            AddEngineCommandValidator validator, IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _messageBus = messageBus;
            _eventTopic = configuration.GetValue<string>(nameof(EngineAddedEvent)) ?? throw new ArgumentNullException($"{nameof(EngineAddedEvent)} configuration value missing");
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
            var newEvent = _mapper.Map<EngineAddedEvent>(engine);
            try
            {
                await _messageBus.PublishMessage(newEvent, _eventTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Writing {newEvent} to message bus failed", newEvent);
                await _repository.DeleteAsync(engine, cancellationToken);
                throw;
            }
            return new Result<AddEngineResponse>(_mapper.Map<AddEngineResponse>(engine));
        }
    }
}
