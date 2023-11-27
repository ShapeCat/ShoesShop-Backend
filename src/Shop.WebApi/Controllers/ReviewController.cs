using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Reviews.Commands;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ReviewController : AbstractController
    {
        public ReviewController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ReviewDto reviewDto)
        {
            var command = Mapper.Map<CreateReviewCommand>(reviewDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
