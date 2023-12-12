using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.FavoriteItems.Commands;
using ShoesShop.Application.Requests.FavoriteItems.OutputVMs;
using ShoesShop.Application.Requests.FavoriteItems.Queries;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class FavoriteItemController : AbstractController
    {
        public FavoriteItemController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Add model variant to favorites
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/FavoriteItem?modelVariantId=3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// Return: ID of created user favorites item
        /// </remarks>
        /// <param name="modelVariantId">Model variant Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Model variant with given id not found</response>
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
        public async Task<ActionResult<Guid>> AddToFavoriteItem([Required] Guid modelVariantId)
        {
            var command = new CreateFavoriteItemCommand()
            {
                ModelVariantId = modelVariantId,
                UserId = UserId,
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get favorites items
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/FavoriteItem
        ///
        /// Return: List of current user favorite items
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FavoriteItemVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FavoriteItemVm>>> GetAll()
        {
            var query = new GetFavoriteItemsByUserQuery()
            {
                UserId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Delete favorite item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/FavoriteItem/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// </remarks>
        /// <param name="favoriteItemId">Favorite item Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Favorite item with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{favoriteItemId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid favoriteItemId)
        {
            var command = new DeleteFavoriteItemCommand()
            {
                FavoriteItemId = favoriteItemId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
