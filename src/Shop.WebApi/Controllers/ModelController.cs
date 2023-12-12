using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Models.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelController : AbstractController
    {
        public ModelController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create model
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Model/
        ///     {
        ///         "name": "Ocean",
        ///         "color": "Black",
        ///         "brand": "Nike",
        ///         "skuId": "MKF12FF",
        ///         "releaseDate": "2023-12-10T17:06:37.461Z"
        ///     }
        ///
        /// Return: Id of created model
        /// </remarks>
        /// <param name="modelDto">Model</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required][FromBody] ModelDto modelDto)
        {
            var command = Mapper.Map<CreateModelCommand>(modelDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all models
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Model
        ///     
        /// Return: List of all models
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelVm>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelVm>>> GetAll()
        {
            var query = new GetAllModelsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get model by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Model/e034b7ef-03c0-4c01-aa00-55a39cbcfcd7
        /// 
        /// Return: Single model with the given Id, if exists
        /// </remarks>
        /// <param name="modelId">Model Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Model with the given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{modelId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Update model
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Model/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "name": "Ocean",
        ///         "color": "Black",
        ///         "brand": "Nike",
        ///         "skuId": "MKF12FF",
        ///         "releaseDate": "2023-12-10T17:06:37.461Z"
        ///     }
        /// 
        /// </remarks>
        /// <param name="modelId">Model Id</param>
        /// <param name="modelDto">New model</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Model with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{modelId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid modelId, [FromBody] ModelDto modelDto)
        {
            var command = Mapper.Map<UpdateModelCommand>(modelDto);
            command.ModelId = modelId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete model
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Model/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="modelId">Model Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Model with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{modelId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid modelId)
        {
            var command = new DeleteModelCommand()
            {
                ModelId = modelId
            };
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get model images
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Model/5c6f2bc5-8168-44ff-9ee9-18526325d923/images
        ///     
        /// Return: List of model images
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Model with the given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{modelId}/images")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ImageVm>>> GetImages([Required] Guid modelId)
        {
            var query = new GetImagesByModelQuery()
            {
                ModelId = modelId,
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
