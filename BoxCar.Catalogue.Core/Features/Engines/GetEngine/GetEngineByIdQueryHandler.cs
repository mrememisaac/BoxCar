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

namespace BoxCar.Catalogue.Core.Features.Engines.GetEngine
{

    public class GetEngineByIdQueryHandler : IRequestHandler<GetEngineByIdQuery, GetEngineByIdResponse>
    {
        private readonly IAsyncRepository<Engine, Guid> _repository;
        private readonly ILogger<GetEngineByIdQueryHandler> _logger;
        private readonly GetEngineByIdQueryValidator _validator;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public GetEngineByIdQueryHandler(IAsyncRepository<Engine, Guid> repository, 
            ILogger<GetEngineByIdQueryHandler> logger, 
            GetEngineByIdQueryValidator validator,
            IDistributedCache cache,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<GetEngineByIdResponse> Handle(GetEngineByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var key = $"{nameof(GetEngineByIdQuery)}-{request.Id}";
            var response = await _cache.GetFromCache<Engine>(key) ?? await _cache.SaveToCache<Engine>(key, await _repository.GetByIdAsync(request.Id, cancellationToken));
            return _mapper.Map<GetEngineByIdResponse>(response);
        }
    }
}
