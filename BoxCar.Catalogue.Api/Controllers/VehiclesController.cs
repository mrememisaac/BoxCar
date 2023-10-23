using BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Catalogue.Api.Models;
using BoxCar.Catalogue.Core.Features.Vehicles.ListVehicles;

namespace BoxCar.Catalogue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VehiclesController(ILogger<VehiclesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetVehicleById))]
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