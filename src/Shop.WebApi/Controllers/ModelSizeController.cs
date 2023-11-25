using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelSizeController : AbstractController
    {
        public ModelSizeController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ModelSizeDto modelSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelSizeDto is null) return BadRequest(ModelState);

            var command = Mapper.Map<CreateModelSizeCommand>(modelSizeDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelSizeVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelSizeVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllModelSizesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{modelSizeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelSizeVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelSizeVm>> GetById(Guid modelSizeId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetModelSizeQuery()
                {
                    ModelSizeId = modelSizeId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{modelSizeId}")]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid modelSizeId, [FromBody] ModelSizeDto modelSizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelSizeDto is null) return BadRequest(ModelState);
            try
            {
                var command = Mapper.Map<UpdateModelSizeCommand>(modelSizeDto);
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
        [Authorize(Policy = "UpdateGoods")]
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
