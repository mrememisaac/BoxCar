using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Admin.Core.Features.OptionPacks.ListOptionPacks;
using BoxCar.Admin.Api.Models;
using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost(Name = nameof(AddOptionPack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddOptionPackResponse>> AddOptionPack(AddOptionPackDto request)
        {
            var response = await _mediator.Send(_mapper.Map<AddOptionPackCommand>(request));
            var routeValues = new
            {
                id = response.Value.Id
            };
            return CreatedAtRoute(nameof(GetOptionPackById), routeValues, response.Value); 
        }

        [HttpGet("{id:guid}", Name = nameof(GetOptionPackById))]
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

        [HttpGet(Name = nameof(ListOptionPacks))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetOptionPackByIdResponse>> ListOptionPacks(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetOptionPackQuery { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}