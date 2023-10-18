using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Engines.AddEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BoxCar.Integration.MessageBus;
using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using Microsoft.Extensions.Configuration;

namespace BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack
{

    public class AddOptionPackCommandHandler : IRequestHandler<AddOptionPackCommand, Result<AddOptionPackResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<AddOptionPackCommandHandler> _logger;
        private readonly AddOptionPackCommandValidator _validator;
        private readonly IMessageBus _messageBus;
        private string _eventTopic;

        public AddOptionPackCommandHandler(IMapper mapper,
            IAsyncRepository<OptionPack, Guid> repository,
            ILogger<AddOptionPackCommandHandler> logger,
            AddOptionPackCommandValidator validator, IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _messageBus = messageBus;
            _eventTopic = configuration.GetValue<string>(nameof(OptionPackAddedEvent)) ?? throw new ArgumentNullException($"{nameof(OptionPackAddedEvent)} configuration value missing");
        }
        public async Task<Result<AddOptionPackResponse>> Handle(AddOptionPackCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding an Engine {request}", request);
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var optionPack = _mapper.Map<OptionPack>(request);
            optionPack = await _repository.CreateAsync(optionPack, cancellationToken);
            var newEvent = _mapper.Map<OptionPackAddedEvent>(optionPack);
            try
            {
                await _messageBus.PublishMessage(newEvent, _eventTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Writing {newEvent} to message bus failed. Rolling back", newEvent);
                await _repository.DeleteAsync(optionPack, cancellationToken);
                throw;
            }
            return new Result<AddOptionPackResponse>(_mapper.Map<AddOptionPackResponse>(optionPack));
        }
    }
}
