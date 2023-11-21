using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Queries;
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
        public async Task<ActionResult> Update(Guid modelVariantId, ModelVariantDto modelVariantDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var command = Mapper.Map<UpdateModelVariantCommand>(modelVariantDto);
                command.ModelVariantId = modelVariantId;
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
                    ModelVariantId = modelVariantId
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{modelVariantId}/Model")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVm>> GetModel(Guid modelVariantId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetModelByVariantQuery()
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

        [HttpGet("{modelVariantId}/Size")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelSizeVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelVm>> GetModelSize(Guid modelVariantId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetModelSizeByVariantQuery()
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

        [HttpPost("{modelVariantId}/Sales")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateSale(Guid modelVariantId, [FromBody] SaleDto saleDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (saleDto is null) return BadRequest(ModelState);

            var command = Mapper.Map<CreateModelVariantSaleCommand>(saleDto);
            command.ModelVariantId = modelVariantId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
