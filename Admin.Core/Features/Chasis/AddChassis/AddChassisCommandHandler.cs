using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Engines.AddEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using BoxCar.Integration.MessageBus;
using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;

namespace BoxCar.Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisCommandHandler : IRequestHandler<AddChassisCommand, Result<AddChassisResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<AddChassisCommandHandler> _logger;
        private readonly AddChassisCommandValidator _validator;
        private readonly IMessageBus _messageBus;

        public AddChassisCommandHandler(IMapper mapper, 
            IAsyncRepository<Chassis,Guid> repository, 
            ILogger<AddChassisCommandHandler> logger,
            AddChassisCommandValidator validator, IMessageBus messageBus)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _messageBus = messageBus;
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
                await _messageBus.PublishMessage(newEvent, nameof(ChassisAddedEvent).ToLower());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Writing {newEvent} to message bus failed. Rolling back", newEvent);
                await _repository.DeleteAsync(chassis, cancellationToken);
                return new Result<AddChassisResponse>(false, "An error occured while writing to message bus");
            }
            return new Result<AddChassisResponse>(_mapper.Map<AddChassisResponse>(chassis));
        }
    }
}
