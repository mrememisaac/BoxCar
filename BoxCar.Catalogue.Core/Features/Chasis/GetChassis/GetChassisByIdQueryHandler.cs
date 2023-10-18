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

namespace BoxCar.Catalogue.Core.Features.Chasis.GetChassis
{

    public class GetChassisByIdQueryHandler : IRequestHandler<GetChassisByIdQuery, GetChassisByIdResponse>
    {
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<GetChassisByIdQueryHandler> _logger;
        private readonly GetChassisByIdQueryValidator _validator;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public GetChassisByIdQueryHandler(IAsyncRepository<Chassis, Guid> repository,
            ILogger<GetChassisByIdQueryHandler> logger,
            GetChassisByIdQueryValidator validator,
            IDistributedCache cache,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<GetChassisByIdResponse> Handle(GetChassisByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var key = $"{nameof(GetChassisByIdQuery)}-{request.Id}";
            var response = await _cache.GetFromCache<Chassis>(key) ?? await _cache.SaveToCache<Chassis>(key, await _repository.GetByIdAsync(request.Id, cancellationToken));
            return _mapper.Map<GetChassisByIdResponse>(response);
        }
    }

}