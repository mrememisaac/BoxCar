using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;

namespace Admin.Core.Features.Vehicles.AddVehicle
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Result<AddVehicleCommand>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Vehicle, Guid> _repository;
        private readonly IAsyncRepository<Chassis, Guid> _chassisRepository;
        private readonly IAsyncRepository<Engine, Guid> _enginesRepository;
        private readonly IAsyncRepository<OptionPack, Guid> _optionPacksRepository;
        private readonly AddVehicleCommandValidator _validator;

        public AddVehicleCommandHandler(IMapper mapper,
            IAsyncRepository<Vehicle, Guid> repository,
            IAsyncRepository<Chassis, Guid> chassisRepository,
            IAsyncRepository<Engine, Guid> enginesRepository,
            IAsyncRepository<OptionPack, Guid> optionPacksRepository,
            AddVehicleCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _chassisRepository = chassisRepository;
            _enginesRepository = enginesRepository;
            _optionPacksRepository = optionPacksRepository;
            _validator = validator;
        }
        public async Task<Result<AddVehicleCommand>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var engine = await _enginesRepository.GetByIdAsync(request.EngineId);
            var chassis = await _chassisRepository.GetByIdAsync(request.ChassisId);
            var optionPack = await _optionPacksRepository.GetByIdAsync(request.OptionPackId);
            var vehicle = new Vehicle(request.Id, engine!, chassis!, optionPack!);
            await _repository.CreateAsync(vehicle);
            return new Result<AddVehicleCommand>(true);
        }
    }
}
