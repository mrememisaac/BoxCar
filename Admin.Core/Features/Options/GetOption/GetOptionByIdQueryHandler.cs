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

namespace BoxCar.Admin.Core.Features.Options.GetOption
{
    public class GetOptionByIdQueryHandler : IRequestHandler<GetOptionByIdQuery, GetOptionByIdResponse>
    {
        private readonly IAsyncRepository<Option, Guid> _repository;
        private readonly ILogger<GetOptionByIdQueryHandler> _logger;
        private readonly GetOptionByIdQueryValidator _validator;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetOptionByIdQueryHandler(IAsyncRepository<Option, Guid> repository,
            ILogger<GetOptionByIdQueryHandler> logger,
            GetOptionByIdQueryValidator validator,
            IMapper mapper,
            IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetOptionByIdResponse> Handle(GetOptionByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var key = $"{nameof(GetOptionByIdQuery)}-{request.Id}";
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            var response = _mapper.Map<GetOptionByIdResponse>(data);
            return await _cache.GetFromCache<GetOptionByIdResponse>(key) ?? await _cache.SaveToCache<GetOptionByIdResponse>(key, response);
        }
    }
}
