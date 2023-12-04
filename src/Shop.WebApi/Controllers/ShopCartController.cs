using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.Queries;
using ShoesShop.Entities;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ShopCartController : AbstractController
    {
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddToShopCart([FromBody] CartItemDto shopCartItemDto)
        {
            var command = Mapper.Map<AddToShopCartCommand>(shopCartItemDto);
            command.UserId = UserId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShopCartItemVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShopCartItemVm>> GetAll()
        {
            var query = new GetShopCartItemsByUserQuery()
            {
                UserId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{shopCartItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShopCartItemVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShopCartItemVm>> GetById(Guid shopCartItemId)
        {
            var query = new GetShopCartItemQuery()
            {
                ShopCartItemId = shopCartItemId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{modelVariantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid modelVariantId, int Amount)
        {
            var command = new UpdateShopCartItemCommand()
            {
                UserId = UserId,
                ModelVariantId = modelVariantId,
                Amount = Amount
            };
            await Mediator.Send(command);
            return NoContent();
        }

        public ShopCartController(IMapper mapper) : base(mapper) { }

        [HttpDelete("{modelVariantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid modelVariantId)
        {
            var command = new DeleteShopCartItemCommand()
            {
                UserId = UserId,
                ModelVariantId = modelVariantId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
