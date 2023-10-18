using BoxCar.Admin.Core.Contracts.Persistence;
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

namespace BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack
{
    public class GetOptionPackByIdQueryHandler : IRequestHandler<GetOptionPackByIdQuery, GetOptionPackByIdResponse>
    {
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<GetOptionPackByIdQueryHandler> _logger;
        private readonly GetOptionPackByIdQueryValidator _validator;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetOptionPackByIdQueryHandler(IAsyncRepository<OptionPack, Guid> repository,
            ILogger<GetOptionPackByIdQueryHandler> logger,
            GetOptionPackByIdQueryValidator validator,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _cache = cache;
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
