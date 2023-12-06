using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.FavoriteItems.Commands;
using ShoesShop.Application.Requests.FavoriteItems.OutputVMs;
using ShoesShop.Application.Requests.FavoriteItems.Queries;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.Queries;
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
        public async Task<ActionResult<Guid>> AddToShopCart([Required] Guid modelVariantId)
        {
            var command = new CreateFavoriteItemCommand()
            {
                ModelVariantId = modelVariantId,
                UserId = UserId,
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FavoriteItemVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteItemVm>> GetAll()
        {
            var query = new GetFavoriteItemsByUserQuery()
            {
                UserId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{favoriteItemId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid favoriteItemId)
        {
            var command = new DeleteFavoriteItemCommand()
            {
                FavoriteItemId = favoriteItemId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
