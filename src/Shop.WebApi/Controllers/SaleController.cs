using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.Sales.Queries;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class SaleController : AbstractController
    {
        public SaleController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SaleVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SaleVm>>> GetAll()
        {
            var query = new GetAllSalesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{saleId}")]
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

        [HttpPut("{saleId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([Required] Guid addressId, [Required] SaleDto saleDto)
        {
            var command = Mapper.Map<UpdateSaleCommand>(saleDto);
            command.SaleId = addressId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{saleId}")]
        [Authorize(Policy = Policies.UpdateGoods)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required] Guid saleId)
        {
            var command = new DeleteSaleCommand()
            {
                SaleId = saleId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
