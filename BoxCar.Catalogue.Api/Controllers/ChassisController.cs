using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        
        [HttpGet("GetChassisById", Name = nameof(GetChassisById))]
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
    }
}