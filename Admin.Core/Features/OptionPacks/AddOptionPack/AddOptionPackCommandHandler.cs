using Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Features.OptionPacks.AddOptionPack
{

    public class AddOptionPackCommandHandler : IRequestHandler<AddOptionPackCommand, Result<AddOptionPackCommand>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<OptionPack, Guid> _repository;
        private readonly ILogger<AddOptionPackCommandHandler> _logger;
        private readonly AddOptionPackCommandValidator _validator;

        public AddOptionPackCommandHandler(IMapper mapper,
            IAsyncRepository<OptionPack, Guid> repository,
            ILogger<AddOptionPackCommandHandler> logger,
            AddOptionPackCommandValidator validator)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<AddOptionPackCommand>> Handle(AddOptionPackCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding an Engine {request}", request);
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var optionPack = _mapper.Map<OptionPack>(request);
            await _repository.CreateAsync(optionPack);
            return new Result<AddOptionPackCommand>(true);
        }
    }
}
