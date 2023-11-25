using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Models.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelController : AbstractController
    {
        public ModelController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ModelDto modelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelDto is null) return BadRequest(ModelState);

            var command = Mapper.Map<CreateModelCommand>(modelDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllModelsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{modelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVm>> GetById(Guid modelId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetModelQuery()
                {
                    ModelId = modelId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{modelId}")]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid addressId, [FromBody] ModelDto modelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelDto is null) return BadRequest(ModelState);
            try
            {
                var command = Mapper.Map<UpdateModelCommand>(modelDto);
                command.ModelId = addressId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{modelId}")]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid modelId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteModelCommand()
                {
                    ModelId = modelId
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{modelId}/images")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelImageVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelImageVm>>> GetImages(Guid modelId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllModelImagesQuery()
            {
                ModelId = modelId,
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{modelId}/images")]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateImage(Guid modelId, [FromBody] ImageDto imageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (imageDto is null) return BadRequest(ModelState);

            var command = Mapper.Map<CreateModelImageCommand>(imageDto);
            command.ModelId = modelId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
