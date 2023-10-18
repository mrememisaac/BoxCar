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

namespace BoxCar.Catalogue.Core.Features.OptionPacks.GetOptionPack
{

    public class GetOptionPackByIdQueryHandler : IRequestHandler<GetOptionPackByIdQuery, GetOptionPackByIdResponse>
    {
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<GetOptionPackByIdQueryHandler> _logger;
        private readonly GetOptionPackByIdQueryValidator _validator;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public GetOptionPackByIdQueryHandler(IAsyncRepository<OptionPack, Guid> repository, 
            ILogger<GetOptionPackByIdQueryHandler> logger, 
            GetOptionPackByIdQueryValidator validator,
            IDistributedCache cache,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<GetOptionPackByIdResponse> Handle(GetOptionPackByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var key = $"{nameof(GetOptionPackByIdQuery)}-{request.Id}";
            var response = await _cache.GetFromCache<OptionPack>(key) ?? await _cache.SaveToCache<OptionPack>(key, await _repository.GetByIdAsync(request.Id, cancellationToken));
            return _mapper.Map<GetOptionPackByIdResponse>(response);
        }
    }
}
