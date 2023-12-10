using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Images.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebApi.Services.Authentication;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ImageController : AbstractController
    {
        public ImageController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateImage([Required] Guid modelId, [Required] ImageDto imageDto)
        {
            var command = Mapper.Map<CreateImageCommand>(imageDto);
            command.ModelId = modelId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ImageVm>>> GetAll()
        {
            var query = new GetAllImagesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ImageVm>> GetById([Required] Guid ImageId)
        {
            var query = new GetImageQuery()
            {
                ImageId = ImageId
            };
            var result = await Mediator.Send(query);
            return Ok(result);

        }

        [HttpPut("{imageId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid imageId, [Required] ImageDto imageDto)
        {
            var command = Mapper.Map<UpdateImageCommand>(imageDto);
            command.ImageId = imageId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{imageId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid imageId)
        {
            var command = new DeleteImageCommand()
            {
                ImageId = imageId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
