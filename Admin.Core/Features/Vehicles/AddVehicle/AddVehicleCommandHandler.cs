using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

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

        public AddVehicleCommandHandler(IMapper mapper,
            IAsyncRepository<Vehicle, Guid> repository,
            IAsyncRepository<Chassis, Guid> chassisRepository,
            IAsyncRepository<Engine, Guid> enginesRepository,
            IAsyncRepository<OptionPack, Guid> optionPacksRepository,
            ILogger<AddVehicleCommandHandler> logger,
            AddVehicleCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _chassisRepository = chassisRepository;
            _enginesRepository = enginesRepository;
            _optionPacksRepository = optionPacksRepository;
            _logger = logger;
            _validator = validator;
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
            var vehicle = new Vehicle(request.Id, engine!, chassis!, optionPack!);
            vehicle = await _repository.CreateAsync(vehicle, cancellationToken);
            return new Result<AddVehicleResponse>(_mapper.Map<AddVehicleResponse>(vehicle));
        }
    }
}
