using BoxCar.Catalogue.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Catalogue.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle
{

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, GetVehicleByIdResponse>
    {
        private readonly IVehicleRepository _repository;
        private readonly ILogger<GetVehicleByIdQueryHandler> _logger;
        private readonly GetVehicleByIdQueryValidator _validator;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(IVehicleRepository repository, 
            ILogger<GetVehicleByIdQueryHandler> logger, 
            GetVehicleByIdQueryValidator validator,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetVehicleByIdResponse> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var response = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetVehicleByIdResponse>(response);
        }
    }
}
