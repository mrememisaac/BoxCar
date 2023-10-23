using BoxCar.Admin.Core.Features.Options.AddOption;
using BoxCar.Admin.Core.Features.Options.GetOption;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Admin.Core.Features.Options.ListOptions;
using BoxCar.Admin.Core.Features.Options.AddOption;
using BoxCar.Admin.Core.Features.Options.GetOption;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/optionpacks/{optionId:guid}/[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly ILogger<OptionsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OptionsController(ILogger<OptionsController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost(Name = nameof(AddOption))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddOptionResponse>> AddOption(Guid optionId, AddOptionDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddOptionCommand>(request));
            var routeValues = new
            {
                id = response.Value.Id
            };
            return CreatedAtRoute(nameof(GetOptionById), routeValues, response.Value); 
        }

        [HttpGet("{id:guid}", Name = nameof(GetOptionById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetOptionByIdResponse>> GetOptionById(Guid id)
        {
            var response = await _mediator.Send(new GetOptionByIdQuery { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet(Name = nameof(ListOptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetOptionByIdResponse>> ListOptions(Guid optionId, int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetOptionQuery { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}