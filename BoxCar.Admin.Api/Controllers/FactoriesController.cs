using BoxCar.Admin.Core.Features.Factories.AddFactory;
using BoxCar.Admin.Core.Features.Factories.GetFactory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactoriesController : ControllerBase
    {
        private readonly ILogger<FactoriesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FactoriesController(ILogger<FactoriesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost(Name = nameof(AddFactory))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddFactoryResponse>> AddFactory(AddFactoryDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddFactoryCommand>(request));
            var routeValues = new
            {
                id = response.Value.Id
            };
            return CreatedAtRoute(nameof(GetFactoryById), routeValues, response.Value);
        }

        [HttpGet("{id:guid}", Name = nameof(GetFactoryById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetFactoryByIdResponse>> GetFactoryById(Guid id)
        {
            var factory = await _mediator.Send(new GetFactoryByIdQuery { Id = id });
            if (factory == null)
            {
                return NotFound();
            }
            return Ok(factory);
        }
    }
}