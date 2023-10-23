using BoxCar.Admin.Core.Features.Engines.AddEngine;
using BoxCar.Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Admin.Core.Features.Engines.ListEngines;
using BoxCar.Admin.Api.Models;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnginesController : ControllerBase
    {
        private readonly ILogger<EnginesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EnginesController(ILogger<EnginesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost(Name = nameof(AddEngine))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddEngineResponse>> AddEngine(AddEngineDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddEngineCommand>(request));
            var routeValues = new
            {
                id = response.Value.Id
            };
            return CreatedAtRoute(nameof(GetEngineById), routeValues, response.Value);            
        }

        [HttpGet("{id:guid}", Name = nameof(GetEngineById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetEngineByIdResponse>> GetEngineById(Guid id)
        {
            var response = await _mediator.Send(new GetEngineByIdQuery { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet(Name = nameof(ListEngines))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetEngineByIdResponse>> ListEngines(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetEngineQuery { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}