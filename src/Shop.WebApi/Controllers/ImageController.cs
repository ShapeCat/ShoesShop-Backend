using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ImageController : AbstractController
    {
        public ImageController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ImageDto imageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (imageDto is null) return BadRequest(ModelState);

            var command = mapper.Map<CreateImageCommand>(imageDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
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

        [HttpPut("{imageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDescription(Guid imageId, [FromBody] ImageDto imageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (imageDto is null) return BadRequest(ModelState);
            try
            {
                var command = mapper.Map<UpdateImageCommand>(imageDto);
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
