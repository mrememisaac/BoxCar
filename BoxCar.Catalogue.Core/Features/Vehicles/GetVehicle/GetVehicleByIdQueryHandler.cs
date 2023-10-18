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
using Microsoft.Extensions.Caching.Distributed;
using BoxCar.Shared.Caching;

namespace BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle
{

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, GetVehicleByIdResponse>
    {
        private readonly IVehicleRepository _repository;
        private readonly ILogger<GetVehicleByIdQueryHandler> _logger;
        private readonly GetVehicleByIdQueryValidator _validator;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(IVehicleRepository repository, 
            ILogger<GetVehicleByIdQueryHandler> logger, 
            GetVehicleByIdQueryValidator validator,
            IDistributedCache cache,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<GetVehicleByIdResponse> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var key = $"{nameof(GetVehicleByIdQuery)}-{request.Id}";
            var response = await _cache.GetFromCache<Vehicle>(key) ?? await _cache.SaveToCache<Vehicle>(key, await _repository.GetByIdAsync(request.Id, cancellationToken));
            return _mapper.Map<GetVehicleByIdResponse>(response);
        }
    }
}
