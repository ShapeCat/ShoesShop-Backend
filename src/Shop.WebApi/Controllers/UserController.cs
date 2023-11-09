using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class UserController : AbstractController
    {
        public UserController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userDto is null) return BadRequest(ModelState);

            var command = mapper.Map<CreateAdressCommand>(userDto);
            var result = await Mediator.Send(command);
            return Ok(result);


        }
    }
}
