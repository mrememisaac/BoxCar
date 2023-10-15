using BoxCar.Admin.Core.Features.Engines.AddEngine;
using BoxCar.Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnginesController : ControllerBase
    {
        private readonly ILogger<EnginesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EnginesController(ILogger<EnginesController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("GetEngineById", Name = nameof(GetEngineById))]
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
    }
}