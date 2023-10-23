using BoxCar.Catalogue.Core.Features.Engines.GetEngine;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BoxCar.Catalogue.Api.Models;
using BoxCar.Catalogue.Core.Features.Engines.ListEngines;

namespace BoxCar.Catalogue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("ListEngines", Name = nameof(ListEngines))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetEngineByIdResponse>> ListEngines(int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetEngineQuery {PageNumber = pageNumber, PageSize = pageSize });
            return Ok(response);
        }
    }
}