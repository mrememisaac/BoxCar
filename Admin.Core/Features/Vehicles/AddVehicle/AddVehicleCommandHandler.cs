using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using BoxCar.Integration.MessageBus;
using BoxCar.Admin.Core.Features.Engines.AddEngine;

namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Result<AddVehicleResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Vehicle, Guid> _repository;
        private readonly IAsyncRepository<Chassis, Guid> _chassisRepository;
        private readonly IAsyncRepository<Engine, Guid> _enginesRepository;
        private readonly IAsyncRepository<OptionPack, Guid> _optionPacksRepository;
        private readonly ILogger<AddVehicleCommandHandler> _logger;
        private readonly AddVehicleCommandValidator _validator;
        private readonly IMessageBus _messageBus;

        public AddVehicleCommandHandler(IMapper mapper,
            IAsyncRepository<Vehicle, Guid> repository,
            IAsyncRepository<Chassis, Guid> chassisRepository,
            IAsyncRepository<Engine, Guid> enginesRepository,
            IAsyncRepository<OptionPack, Guid> optionPacksRepository,
            ILogger<AddVehicleCommandHandler> logger,
            AddVehicleCommandValidator validator, IMessageBus messageBus)
        {
            _mapper = mapper;
            _repository = repository;
            _chassisRepository = chassisRepository;
            _enginesRepository = enginesRepository;
            _optionPacksRepository = optionPacksRepository;
            _logger = logger;
            _validator = validator;
            _messageBus = messageBus;
        }
        public async Task<Result<AddVehicleResponse>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding a vehicle {request}", request); 
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var engine = await _enginesRepository.GetByIdAsync(request.EngineId, cancellationToken);
            var chassis = await _chassisRepository.GetByIdAsync(request.ChassisId, cancellationToken);
            var optionPack = await _optionPacksRepository.GetByIdAsync(request.OptionPackId, cancellationToken);
            var vehicle = new Vehicle(request.Id, engine!, chassis!, optionPack!, request.Price);
            vehicle = await _repository.CreateAsync(vehicle, cancellationToken);
            var newEvent = _mapper.Map<EngineAddedEvent>(engine);
            try
            {
                await _messageBus.PublishMessage(newEvent, "eventupdatedmessage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Writing {newEvent} to message bus failed. Rolling back", newEvent);
                await _repository.DeleteAsync(vehicle, cancellationToken);
                return new Result<AddVehicleResponse>(false, "An error occured while writing to message bus");
            }
            return new Result<AddVehicleResponse>(_mapper.Map<AddVehicleResponse>(vehicle));
        }
    }
}
