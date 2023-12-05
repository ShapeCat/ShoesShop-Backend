using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Models.Queries;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelController : AbstractController
    {
        public ModelController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required][FromBody] ModelDto modelDto)
        {
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
            var query = new GetAllModelsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{modelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVm>> GetById([Required] Guid modelId)
        {
            var query = new GetModelQuery()
            {
                ModelId = modelId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{modelId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid addressId, [FromBody] ModelDto modelDto)
        {
            var command = Mapper.Map<UpdateModelCommand>(modelDto);
            command.ModelId = addressId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{modelId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid modelId)
        {
            var command = new DeleteModelCommand()
            {
                ModelId = modelId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{modelId}/images")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelImageVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelImageVm>>> GetImages([Required] Guid modelId)
        {
            var query = new GetAllModelImagesQuery()
            {
                ModelId = modelId,
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{modelId}/images")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateImage([Required] Guid modelId, [Required] ImageDto imageDto)
        {
            var command = Mapper.Map<CreateModelImageCommand>(imageDto);
            command.ModelId = modelId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
