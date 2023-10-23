using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using BoxCar.Admin.Core.Features.Warehouses.GetWareHouse;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehousesController : ControllerBase
    {
        private readonly ILogger<WarehousesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WarehousesController(ILogger<WarehousesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("AddWareHouse", Name = nameof(AddWareHouse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddWareHouseResponse>> AddWareHouse(AddWareHouseDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddWareHouseCommand>(request));
            return CreatedAtAction(nameof(GetWareHouseById), response.Value);
        }

        [HttpGet("GetWareHouseById", Name = nameof(GetWareHouseById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetWareHouseByIdResponse>> GetWareHouseById(Guid id)
        {
            var response = await _mediator.Send(new GetWareHouseByIdQuery { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}