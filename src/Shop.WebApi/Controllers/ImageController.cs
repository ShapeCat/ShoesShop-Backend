using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Images.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ImageController : AbstractController
    {
        public ImageController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create model image
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Image?modelId=3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     {
        ///         "modelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "isPreview": true,
        ///         "url": "https://ibb.co/CHqFHrk"
        ///     }
        ///
        /// Return: Id of created image
        /// </remarks>
        /// <param name="modelId">Model Id</param>
        /// <param name="imageDto">Image</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Model with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateImage([Required] Guid modelId, [Required] ImageDto imageDto)
        {
            var command = Mapper.Map<CreateImageCommand>(imageDto);
            command.ModelId = modelId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all images
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Image
        ///
        /// Return: List of all images
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ImageVm>>> GetAll()
        {
            var query = new GetAllImagesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Image?imageId=3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// Return: Single image with the given Id, if exists
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Image with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{imageId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ImageVm>> GetById([Required] Guid imageId)
        {
            var query = new GetImageQuery()
            {
                ImageId = imageId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update image
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Image/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///         "modelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "isPreview": true,
        ///         "url": "https://ibb.co/CHqFHrk"
        ///     }
        /// 
        /// </remarks>
        /// <param name="imageId">Image Id</param>
        /// <param name="imageDto">New image</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Image with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{imageId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid imageId, [Required] ImageDto imageDto)
        {
            var command = Mapper.Map<UpdateImageCommand>(imageDto);
            command.ImageId = imageId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Image/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="imageId">Image Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Image with the given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{imageId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid imageId)
        {
            var command = new DeleteImageCommand()
            {
                ImageId = imageId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
