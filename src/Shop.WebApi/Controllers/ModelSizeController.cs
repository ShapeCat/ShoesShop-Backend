using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelSizeController : AbstractController
    {
        public ModelSizeController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create model size
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ModelSize/
        ///     {
        ///         "size": 15
        ///     }
        ///
        /// Return: ID of created model size
        /// </remarks>
        /// <param name="modelSizeDto">Model size</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="409">Same model size already exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] ModelSizeDto modelSizeDto)
        {
            var command = Mapper.Map<CreateModelSizeCommand>(modelSizeDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all model sizes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/ModelSize
        ///     
        /// Return: List of all model sizes
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModelSizeVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelSizeVm>>> GetAll()
        {
            var query = new GetAllModelSizesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get model size by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ModelSize
        ///
        /// Return: Single model size with given id
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="404">Model size with given Id not exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{modelSizeId}")]
        [AllowAnonymous]
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

        /// <summary>
        /// Update model size
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/ModelSize/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "size": 15
        ///     }
        /// 
        /// </remarks>
        /// <param name="modelSizeId">Model size Id</param>
        /// <param name="modelSizeDto">New model size</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Model size with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{modelSizeId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid modelSizeId, [Required] ModelSizeDto modelSizeDto)
        {
            var command = Mapper.Map<UpdateModelSizeCommand>(modelSizeDto);
            command.ModelSizeId = modelSizeId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete model size
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/ModelSize/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="modelSizeId">Model size Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Model size with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{modelSizeId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid modelSizeId)
        {
            var command = new DeleteModelSizeCommand()
            {
                ModelSizeId = modelSizeId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
