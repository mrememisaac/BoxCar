using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionPacksController : ControllerBase
    {
        private readonly ILogger<OptionPacksController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OptionPacksController(ILogger<OptionPacksController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost("AddOptionPack", Name = nameof(AddOptionPack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddOptionPackResponse>> AddOptionPack(AddOptionPackDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddOptionPackCommand>(request));
            return CreatedAtAction(nameof(GetOptionPackById), response.Value);
        }

        [HttpGet("GetOptionPackById", Name = nameof(GetOptionPackById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetOptionPackByIdResponse>> GetOptionPackById(Guid id)
        {
            var response = await _mediator.Send(new GetOptionPackByIdQuery { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}