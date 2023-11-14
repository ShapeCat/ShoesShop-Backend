using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelSizeController : AbstractController
    {
        public ModelSizeController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ModelSizeDto modelSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelSizeDto is null) return BadRequest(ModelState);

            var command = mapper.Map<CreateModelCommand>(modelSizeDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{modelSizeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDescription(Guid modelSizeId, [FromBody] ModelSizeDto modelSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelSizeDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateModelSizeCommand>(modelSizeDto);
                command.ModelSizeId = modelSizeId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{modelSizeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid modelSizeId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteModelSizeCommand()
                {
                    ModelSizeId = modelSizeId
                };
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
