using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.Queries;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Queries;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelVariantController : AbstractController
    {
        public ModelVariantController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] ModelVariantDto modelVariantDto)
        {
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
        public async Task<ActionResult<ModelVariantVm>> GetById([Required] Guid modelVariantId)
        {
            var query = new GetModelVariantQuery()
            {
                ModelVariantId = modelVariantId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{modelVariantId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid modelVariantId, [Required] ModelVariantDto modelVariantDto)
        {
            var command = Mapper.Map<UpdateModelVariantCommand>(modelVariantDto);
            command.ModelVariantId = modelVariantId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{modelVariantId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid modelVariantId)
        {
            var command = new DeleteModelVariantCommand()
            {
                ModelVariantId = modelVariantId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{modelVariantId}/Model")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVm>> GetModel([Required] Guid modelVariantId)
        {
            var query = new GetModelByVariantQuery()
            {
                ModelVariantId = modelVariantId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{modelVariantId}/Size")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelSizeVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVm>> GetModelSize([Required] Guid modelVariantId)
        {
            var query = new GetModelSizeByVariantQuery()
            {
                ModelVariantId = modelVariantId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{modelVariantId}/Sales")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateSale([Required] Guid modelVariantId, [Required] SaleDto saleDto)
        {
            var command = Mapper.Map<CreateModelVariantSaleCommand>(saleDto);
            command.ModelVariantId = modelVariantId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
