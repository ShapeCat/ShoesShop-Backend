using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelVariantController : AbstractController
    {
        public ModelVariantController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ModelVariantDto modelVariantDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelVariantDto is null) return BadRequest(ModelState);

            var command = Mapper.Map<CreateModelVariantCommand>(modelVariantDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelVariantVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelVariantVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllModelVariantQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{modelVariantId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelVariantVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVariantVm>> GetById(Guid modelVariantId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetModelVariantQuery()
                {
                    ModelVariantId = modelVariantId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{modelVariantId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStock(Guid modelVariantId, int ItemsLeft)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (ItemsLeft < 0) return BadRequest(ModelState);
            try
            {
                var command = new UpdateModelVariantCommand()
                {
                    ModelVariantId = modelVariantId,
                    ItemsLeft = ItemsLeft,
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{modelVariantId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid modelVariantId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteModelVariantCommand()
                {
                    ModelvariantId = modelVariantId
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
