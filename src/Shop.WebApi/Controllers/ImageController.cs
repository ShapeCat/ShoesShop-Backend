using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Images.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ImageController : AbstractController
    {
        public ImageController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ImageVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllImagesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ImageVm>> GetById(Guid ImageId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetImageQuery()
                {
                    ImageId = ImageId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{imageId}")]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid imageId, [FromBody] ImageDto imageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (imageDto is null) return BadRequest(ModelState);
            try
            {
                var command = Mapper.Map<UpdateImageCommand>(imageDto);
                command.ImageId = imageId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{imageId}")]
        [Authorize(Policy = "UpdateGoods")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid imageId)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var command = new DeleteImageCommand()
                {
                    ImageId = imageId
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
