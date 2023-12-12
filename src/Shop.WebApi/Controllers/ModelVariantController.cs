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
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelVariantController : AbstractController
    {
        public ModelVariantController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create model variant
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ModelVariant/
        ///     {
        ///         "modelId": "e034b7ef-03c0-4c01-aa00-55a39cbcfcd7",
        ///         "modelSizeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "itemsLeft": 1,
        ///         "price": 5000
        ///     }
        ///
        /// Return: ID of created model variant
        /// </remarks>
        /// <param name="modelVariantDto">Model variant</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="409">Same model variant already exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] ModelVariantDto modelVariantDto)
        {
            var command = Mapper.Map<CreateModelVariantCommand>(modelVariantDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all model variants
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/ModelVariant
        ///     
        /// Return: List of all model variants
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [AllowAnonymous]
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

        /// <summary>
        /// Get model variant by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ModelVariant
        ///
        /// Return: Single address with the given Id, if exists
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="404">Model variant with given Id not exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{modelVariantId}")]
        [AllowAnonymous]
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

        /// <summary>
        /// Update model variant
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/ModelVariant/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "modelId": "e034b7ef-03c0-4c01-aa00-55a39cbcfcd7",
        ///         "modelSizeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "itemsLeft": 1,
        ///         "price": 5000
        ///     }
        /// 
        /// Warning: Model Id and model size do not updates and will be removed in future
        /// </remarks>
        /// <param name="modelVariantId">Model variant Id</param>
        /// <param name="modelVariantDto">New model variant</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Model variant with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{modelVariantId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid modelVariantId, [Required] ModelVariantDto modelVariantDto)
        {
            var command = Mapper.Map<UpdateModelVariantCommand>(modelVariantDto);
            command.ModelVariantId = modelVariantId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete model variant
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/ModelVariant/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="modelVariantId">Model variant Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Model variant with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{modelVariantId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            return Ok();
        }

        /// <summary>
        /// Get model by model variant
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ModelVariant/5c6f2bc5-8168-44ff-9ee9-18526325d923/model
        ///
        /// Return: Model of given model variant
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="404">Model variant with given Id not exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{modelVariantId}/model")]
        [AllowAnonymous]
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

        /// <summary>
        /// Get size by model variant
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ModelVariant/5c6f2bc5-8168-44ff-9ee9-18526325d923/size
        ///
        /// Return: Size of given model variant
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="404">Model variant with given Id not exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{modelVariantId}/size")]
        [AllowAnonymous]
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
    }
}
