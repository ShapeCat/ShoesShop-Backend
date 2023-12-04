using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.FavoriteItems.Commands;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class FavoriteItemController : AbstractController
    {
        public FavoriteItemController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddToShopCart(Guid modelVariantId)
        {
            var command = new CreateFavoriteItemCommand()
            {
                ModelVariantId = modelVariantId,
                UserId = UserId,
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
