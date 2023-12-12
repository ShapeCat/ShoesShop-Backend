using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Addresses.Queries;
using ShoesShop.Application.Requests.Users.Command;
using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Application.Requests.Users.Queries;
using ShoesShop.Entities;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class UserController : AbstractController
    {
        public UserController(IMapper mapper) : base(mapper) { }

        /// <summary>
        /// Get user 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/User
        ///
        /// Return: Current user information
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserVm))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Update user info
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/User/
        ///     {
        ///         "addressId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "userName": "string",
        ///         "phone": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="userDto">New user info</param>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([FromBody] UserDto userDto)
        {
            var command = Mapper.Map<UpdateUserCommand>(userDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get user address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/User/address
        /// 
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="404">Address of current user not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("address")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressVm))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Get all users by role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/User/role?role=Manager
        /// 
        /// Return: List of users with specific roles
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpGet("role")]
        [Authorize(Policy = Policies.UpdateRoles)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserVm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserVm>>> GetAllByRole([Required] Roles role)
        {
            var query = new GetUsersByRoleQuery()
            {
                Role = role
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update user role
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/User/role?userId=3fa85f64-5717-4562-b3fc-2c963f66afa6&#38;role=User
        /// 
        /// </remarks>
        /// <response code="200">Successful Operation</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="401">Authorization Error. Login first for this action</response>
        /// <response code="403">User not allowed for this action</response>
        /// <response code="404">User with given Id not found</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPut("role")]
        [Authorize(Policy = Policies.UpdateRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateRole([Required] Guid userId, [Required] Roles role)
        {
            var command = new UpdateUserRoleCommand()
            {
                UserId = userId,
                Role = role,
            };
            await Mediator.Send(command);
            return Ok();
        }
    }
}
