using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.Sales.Queries;
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllSalesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
