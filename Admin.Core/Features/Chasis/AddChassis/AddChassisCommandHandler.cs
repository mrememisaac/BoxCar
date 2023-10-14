using Admin.Core.Contracts.Persistence;
using Admin.Core.Features.Engines.AddEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisCommandHandler : IRequestHandler<AddChassisCommand, Result<AddChassisResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<AddChassisCommandHandler> _logger;
        private readonly AddChassisCommandValidator _validator;

        public AddChassisCommandHandler(IMapper mapper, 
            IAsyncRepository<Chassis,Guid> repository, 
            ILogger<AddChassisCommandHandler> logger,
            AddChassisCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
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
            await _repository.CreateAsync(chassis, cancellationToken);
            return new Result<AddChassisResponse>(_mapper.Map<AddChassisResponse>(chassis));
        }
    }
}
