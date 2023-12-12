using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.Sales.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class SaleController : AbstractController
    {
        public SaleController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Create sale for model variant
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Sale?modelVariantId=3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     {
        ///       "percent": 5,
        ///       "saleEndDate": "2023-12-12T00:46:25.064Z"
        ///     }
        ///
        /// Return: ID of created review
        /// </remarks>
        /// <param name="modelVariantId">Model variant id</param>
        /// <param name="saleDto">Review</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Model variant with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([Required] Guid modelVariantId, [Required] SaleDto saleDto)
        {
            var command = Mapper.Map<CreateSaleCommand>(saleDto);
            command.ModelVariantId = modelVariantId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all sales
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Sale
        ///     
        /// Return: List of all sales
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SaleVm>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SaleVm>>> GetAll()
        {
            var query = new GetAllSalesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get sale by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Sale
        ///
        /// Return: Single sale with the given Id, if exists
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="404">Sale with given Id not exists</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("{saleId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SaleVm>> GetById([Required] Guid saleId)
        {
            var query = new GetSaleQuery()
            {
                SaleId = saleId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update sale
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Sale/5da752ea-3a18-4bd9-aaca-66519c23a049
        ///     {
        ///       "percent": 10,
        ///       "saleEndDate": "2023-12-12T00:46:25.064Z"
        ///     }
        /// 
        /// </remarks>
        /// <param name="saleId">Sale Id</param>
        /// <param name="saleDto">New sale</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">Sale with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("{saleId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid saleId, [Required] SaleDto saleDto)
        {
            var command = Mapper.Map<UpdateSaleCommand>(saleDto);
            command.SaleId = saleId;
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
        /// <param name="saleId">Sale Id</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="404">Sale with the given Id not found</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpDelete("{saleId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid saleId)
        {
            var command = new DeleteSaleCommand()
            {
                SaleId = saleId
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
