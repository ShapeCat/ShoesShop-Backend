using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Addresses.Queries;
using ShoesShop.Application.Requests.Users.Command;
using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Application.Requests.Users.Queries;
using ShoesShop.Entities;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class UserController : AbstractController
    {
        public UserController(IMapper mapper) : base(mapper) { }

        [HttpGet("role")]
        [Authorize(Policy = Policies.UpdateRoles)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserVm>>> GetAllByRole(Roles role)
        {
            var query = new GetUsersByRoleQuery()
            {
                Role = role
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserVm>> GetById()
        {
            var query = new GetUserQuery()
            {
                UserId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([FromBody] UserDto userDto)
        {
            var command = Mapper.Map<UpdateUserCommand>(userDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{userId}/role")]
        [Authorize(Policy = Policies.UpdateRoles)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateRole(Guid userId, Roles role)
        {
            var command = new UpdateUserRoleCommand()
            {
                UserId = userId,
                Role = role,
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("address")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressVm>> GetAddress()
        {
            var query = new GetAddressByUserQuery()
            {
                UserId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
