using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Admin.Api.Models;
using BoxCar.Admin.Core.Features.Chasis.ListChassis;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChassisController : ControllerBase
    {
        private readonly ILogger<ChassisController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ChassisController(ILogger<ChassisController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost(Name = nameof(AddChassis))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddChassisResponse>> AddChassis(AddChassisDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddChassisCommand>(request));
            var routeValues = new
            {
                id = response.Value.Id
            };
            return CreatedAtRoute(nameof(GetChassisById), routeValues,  response.Value);
        }

        [HttpGet("{id:guid}", Name = nameof(GetChassisById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetChassisByIdResponse>> GetChassisById(Guid id)
        {
            var response = await _mediator.Send(new GetChassisByIdQuery { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet(Name = nameof(GetChassis))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetChassisQueryResponse>> GetChassis(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetChassisQuery { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}