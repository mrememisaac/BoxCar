using BoxCar.Catalogue.Core.Features.OptionPacks.GetOptionPack;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoxCar.Catalogue.Api.Controllers
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
            _mapper = mapper;
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