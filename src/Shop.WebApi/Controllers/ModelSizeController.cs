using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebApi.Services.Authentication;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelSizeController : AbstractController
    {
        public ModelSizeController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] ModelSizeDto modelSizeDto)
        {
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
            var query = new GetAllModelSizesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{modelSizeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelSizeVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelSizeVm>> GetById([Required] Guid modelSizeId)
        {
            var query = new GetModelSizeQuery()
            {
                ModelSizeId = modelSizeId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{modelSizeId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid modelSizeId, [Required] ModelSizeDto modelSizeDto)
        {
            var command = Mapper.Map<UpdateModelSizeCommand>(modelSizeDto);
            command.ModelSizeId = modelSizeId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{modelSizeId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid modelSizeId)
        {
            var command = new DeleteModelSizeCommand()
            {
                ModelSizeId = modelSizeId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
