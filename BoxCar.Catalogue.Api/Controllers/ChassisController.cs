using BoxCar.Catalogue.Core.Features.Chasis.GetChassis;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Catalogue.Api.Models;
using BoxCar.Catalogue.Core.Features.Chasis.ListChassis;

namespace BoxCar.Catalogue.Api.Controllers
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
            _mapper = mapper;
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

        [HttpGet("ListChassis", Name = nameof(ListChassis))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetChassisByIdResponse>> ListChassis(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetChassisQuery {PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}