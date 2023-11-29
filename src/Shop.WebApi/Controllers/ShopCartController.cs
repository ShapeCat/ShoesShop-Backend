using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ShopCartController : AbstractController
    {
        public ShopCartController(IMapper mapper) : base(mapper) { }

        [HttpDelete("{shopCartItemId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid shopCartItemId)
        {
            var command = new DeleteShopCartItemCommand()
            {
                ShopCartItemId = shopCartItemId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
