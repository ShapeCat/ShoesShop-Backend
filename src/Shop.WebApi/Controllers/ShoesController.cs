using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.WebApi.Dto;

namespace ShoesShop.WebAPI.Controllers
{
    public class ShoesController : ControllerAbstract
    {
        public ShoesController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoesVm>))]
        public async Task<ActionResult<IEnumerable<ShoesVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllShoesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{shoesId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ShoesVm))]
        public async Task<ActionResult<ShoesVm>> GetShoes(Guid shoesId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetShoesQuery()
                {
                    ShoesId = shoesId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{shoesId}/sizes")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoesVm>))]
        public async Task<ActionResult<IEnumerable<ShoesVm>>> GetShoesSizes(Guid shoesId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetShoesSizesByShoesQuery()
                {
                    ShoesId = shoesId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{shoesId}/description")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DescriptionVm>))]
        public async Task<ActionResult<IEnumerable<ShoesVm>>> GetShoesDescription(Guid shoesId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetDescriptionByShoesQuery()
                {
                    ShoesId = shoesId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> CreateShoes([FromBody] ShoesDto shoesDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (shoesDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<CreateShoesCommand>(shoesDto);
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (AlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{shoesId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteShoes(Guid shoesId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteShoesCommand()
                {
                    ShoesId = shoesId
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{shoesId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateShoes(Guid shoesId, [FromBody] ShoesDto shoesDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (shoesDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateShoesCommand>(shoesDto);
                command.ShoesId = shoesId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}