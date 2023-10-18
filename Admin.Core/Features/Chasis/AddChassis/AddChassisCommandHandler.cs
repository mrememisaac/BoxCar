using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Engines.AddEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using BoxCar.Integration.MessageBus;
using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using Microsoft.Extensions.Configuration;

namespace BoxCar.Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisCommandHandler : IRequestHandler<AddChassisCommand, Result<AddChassisResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<AddChassisCommandHandler> _logger;
        private readonly AddChassisCommandValidator _validator;
        private readonly IMessageBus _messageBus;
        private string _eventTopic;

        public AddChassisCommandHandler(IMapper mapper, 
            IAsyncRepository<Chassis,Guid> repository, 
            ILogger<AddChassisCommandHandler> logger,
            AddChassisCommandValidator validator, IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _messageBus = messageBus;
            _eventTopic = configuration.GetValue<string>(nameof(ChassisAddedEvent)) ?? throw new ArgumentNullException($"{nameof(ChassisAddedEvent)} configuration value missing");
        }
        public async Task<Result<AddChassisResponse>> Handle(AddChassisCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a Chassis {request}", request);
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var chassis = _mapper.Map<Chassis>(request);
            chassis = await _repository.CreateAsync(chassis, cancellationToken);
            var newEvent = _mapper.Map<ChassisAddedEvent>(chassis);
            try
            {
                await _messageBus.PublishMessage(newEvent, _eventTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Writing {newEvent} to message bus failed. Rolling back", newEvent);
                await _repository.DeleteAsync(chassis, cancellationToken);
                throw;
            }
            return new Result<AddChassisResponse>(_mapper.Map<AddChassisResponse>(chassis));
        }
    }
}
