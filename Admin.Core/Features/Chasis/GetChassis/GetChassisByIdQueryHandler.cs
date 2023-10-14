using Admin.Core.Contracts.Persistence;
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

namespace Admin.Core.Features.Chasis.GetChassis
{

    public class GetChassisByIdQueryHandler : IRequestHandler<GetChassisByIdQuery, GetChassisByIdResponse>
    {
        private readonly IAsyncRepository<Chassis, Guid> _repository;
        private readonly ILogger<GetChassisByIdQueryHandler> _logger;
        private readonly GetChassisByIdQueryValidator _validator;
        private readonly IMapper _mapper;

        public GetChassisByIdQueryHandler(IAsyncRepository<Chassis, Guid> repository,
            ILogger<GetChassisByIdQueryHandler> logger,
            GetChassisByIdQueryValidator validator,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetChassisByIdResponse> Handle(GetChassisByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var response = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetChassisByIdResponse>(response);
        }
    }
}
