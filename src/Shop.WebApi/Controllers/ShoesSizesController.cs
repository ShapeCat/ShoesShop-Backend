using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.WebApi.Dto;

namespace ShoesShop.WebAPI.Controllers
{
    public class ShoesSizesController : ControllerAbstract
    {
        public ShoesSizesController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoesSizeVm>))]
        public async Task<ActionResult<IEnumerable<ShoesSizeVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllShoesSizesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{shoesSizeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ShoesSizeVm))]
        public async Task<ActionResult<ShoesSizeVm>> GetShoesSize(Guid shoesSizeId)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetShoesSizeQuery()
                {
                    ShoesSizeId = shoesSizeId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{shoesSizeId}")]
        [ProducesResponseType(409)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> CreateDescription(Guid shoesSizeId, [FromBody] ShoesSizeDto shoesSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (shoesSizeDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<CreateShoesSizeCommand>(shoesSizeDto);
                command.ShoesId = shoesSizeId;
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (AlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{shoesSizeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteShoesSize(Guid shoesSizeId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteShoesSizeCommand()
                {
                    ShoesSizeId = shoesSizeId
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateShoes(Guid id, [FromBody] ShoesSizeDto shoesSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (shoesSizeDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateShoesSizeCommand>(shoesSizeDto);
                command.ShoesSizeId = id;
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