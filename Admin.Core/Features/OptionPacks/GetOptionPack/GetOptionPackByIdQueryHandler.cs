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

namespace BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack
{

    public class GetOptionPackByIdQueryHandler : IRequestHandler<GetOptionPackByIdQuery, GetOptionPackByIdResponse>
    {
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<GetOptionPackByIdQueryHandler> _logger;
        private readonly GetOptionPackByIdQueryValidator _validator;
        private readonly IMapper _mapper;

        public GetOptionPackByIdQueryHandler(IAsyncRepository<OptionPack, Guid> repository, 
            ILogger<GetOptionPackByIdQueryHandler> logger, 
            GetOptionPackByIdQueryValidator validator,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetOptionPackByIdResponse> Handle(GetOptionPackByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var response = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetOptionPackByIdResponse>(response);
        }
    }
}
