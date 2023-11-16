using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Prices.Commands;
using ShoesShop.Application.Requests.Prices.Queries;

namespace ShoesShop.WebApi.Controllers
{
    public class PriceController : AbstractController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PriceVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PriceVm>>> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var query = new GetAllPricesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{priceId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PriceVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PriceVm>> GetById(Guid priceId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var query = new GetPriceQuery()
                {
                    PriceId = priceId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        public PriceController(IMapper mapper) : base(mapper) { }

        [HttpPut("{priceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDescription(Guid priceId, [FromBody] PriceDto priceDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (priceDto is null) return BadRequest(ModelState);
            try
            {
                var command = Mapper.Map<UpdatePriceCommand>(priceDto);
                command.PriceId = priceId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
