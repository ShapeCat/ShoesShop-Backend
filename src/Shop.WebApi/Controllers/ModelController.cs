using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class ModelController : AbstractController
    {
        public ModelController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] ModelDto modelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (modelDto is null) return BadRequest(ModelState);

            var command = mapper.Map<CreateModelCommand>(modelDto);
            var result = await Mediator.Send(command);
            return Ok(result);


        }
    }
}
