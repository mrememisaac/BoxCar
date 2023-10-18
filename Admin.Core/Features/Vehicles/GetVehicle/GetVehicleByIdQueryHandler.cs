﻿using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
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

namespace BoxCar.Admin.Core.Features.Vehicles.GetVehicle
{

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, GetVehicleByIdResponse>
    {
        private readonly IAsyncRepository<Vehicle, Guid> _repository;
        private readonly ILogger<GetVehicleByIdQueryHandler> _logger;
        private readonly GetVehicleByIdQueryValidator _validator;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetVehicleByIdQueryHandler(IAsyncRepository<Vehicle, Guid> repository,
            ILogger<GetVehicleByIdQueryHandler> logger,
            GetVehicleByIdQueryValidator validator,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _cache = cache;
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
