using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ShopCartController : AbstractController
    {
        public ShopCartController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Add model variant to shop cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ShopCart
        ///
        /// Return: ID of created user shop cart item
        /// </remarks>
        /// <param name="shopCartItemDto">Shop cart item</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Model variant with given Id not found</response>
        /// <response code="409">Given model variant already added</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] CartItemDto shopCartItemDto)
        {
            var command = Mapper.Map<AddToShopCartCommand>(shopCartItemDto);
            command.UserId = UserId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get shop cart items
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/ShopCart
        ///
        /// Return: List of current user shop cart items
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShopCartItemVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Delete shop cart item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/ShopCart/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// </remarks>
        /// <param name="shopCartItemId">Shop cart item Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Shop cart item with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{shopCartItemId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShopCartItemVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShopCartItemVm>> GetById([Required] Guid shopCartItemId)
        {
            var query = new GetShopCartItemQuery()
            {
                ShopCartItemId = shopCartItemId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update shop cart item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/ShopCart/3fa85f64-5717-4562-b3fc-2c963f66afa6?Amount=12
        /// 
        /// </remarks>
        /// <param name="modelVariantId">Model variant id</param>
        /// <param name="Amount">New amount</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Shop cart item with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{modelVariantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid modelVariantId, [Required] int Amount)
        {
            var command = new UpdateShopCartItemCommand()
            {
                UserId = UserId,
                ModelVariantId = modelVariantId,
                Amount = Amount
            };
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Delete sale
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Sale/5c6f2bc5-8168-44ff-9ee9-18526325d923
        /// 
        /// </remarks>
        /// <param name="modelVariantId">Sale Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Sale with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{modelVariantId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid modelVariantId)
        {
            var command = new DeleteShopCartItemCommand()
            {
                UserId = UserId,
                ModelVariantId = modelVariantId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
