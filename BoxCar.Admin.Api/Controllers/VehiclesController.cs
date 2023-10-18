using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Admin.Core.Features.Vehicles.ListVehicles;
using BoxCar.Admin.Api.Models;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VehiclesController(ILogger<VehiclesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost("AddVehicle", Name = nameof(AddVehicle))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddVehicleResponse>> AddVehicle(AddVehicleDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddVehicleCommand>(request));
            return CreatedAtAction(nameof(GetVehicleById), response.Value);
        }

        [HttpGet("GetVehicleById", Name = nameof(GetVehicleById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetVehicleByIdResponse>> GetVehicleById(Guid id)
        {
            var response = await _mediator.Send(new GetVehicleByIdQuery { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("ListVehicles", Name = nameof(ListVehicles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetVehicleByIdResponse>> ListVehicles(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetVehicleQuery {PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}