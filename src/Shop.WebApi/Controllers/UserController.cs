using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Common.Exceptions;
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
            try
            {
                var query = new GetUsersByRoleQuery()
                {
                    Role = role
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserVm>> GetById()
        {
            try
            {
                var query = new GetUserQuery()
                {
                    UserId = UserId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([FromBody] UserDto userDto)
        {
            try
            {
                var command = Mapper.Map<UpdateUserCommand>(userDto);
                command.UserId = UserId;
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpPut("{userId}/role")]
        [Authorize(Policy = Policies.UpdateRoles)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateRole(Guid userId, Roles role)
        {
            try
            {
                var command = new UpdateUserRoleCommand()
                {
                    UserId = userId,
                    Role = role,
                };
                await Mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressVm))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressVm>> GetAddress()
        {
            try
            {
                var query = new GetAddressByUserQuery()
                {
                    UserId = UserId
                };
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }
    }
}
