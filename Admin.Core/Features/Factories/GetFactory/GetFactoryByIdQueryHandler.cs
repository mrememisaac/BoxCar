using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Core.Features.Factories.GetFactory
{

    public class GetFactoryByIdQueryHandler : IRequestHandler<GetFactoryByIdQuery, GetFactoryByIdResponse>
    {
        private readonly IAsyncRepository<Factory, Guid> _repository;
        private readonly ILogger<GetFactoryByIdQueryHandler> _logger;
        private readonly GetFactoryByIdQueryValidator _validator;
        private readonly IMapper _mapper;

        public GetFactoryByIdQueryHandler(IAsyncRepository<Factory, Guid> repository, 
            ILogger<GetFactoryByIdQueryHandler> logger, 
            GetFactoryByIdQueryValidator validator,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetFactoryByIdResponse> Handle(GetFactoryByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var response = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetFactoryByIdResponse>(response);
        }
    }
}
