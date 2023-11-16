using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class PriceController : AbstractController
    {
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
