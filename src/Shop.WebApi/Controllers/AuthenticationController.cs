using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Authentication.Commands;
using ShoesShop.Application.Requests.Authentication.Queries;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    /// <summary>
    /// Login/Register user 
    /// </summary>
    public class AuthenticationController : AbstractController
    {
        private readonly ITokenService tokenService;

        public AuthenticationController(IMapper mapper, ITokenService tokenService) : base(mapper) => this.tokenService = tokenService;

        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post /api/Authentication/
        ///     {
        ///         "login": "login1",
        ///         "password": "qwerty"
        ///     }
        /// 
        /// </remarks>
        /// <param name="user">User login data</param>
        /// <response code="201">Successful Operation</response>
        /// <response code="409">User with this login already exists</response>
        /// <response code="400">Validation Error. Check given data</response>
        /// <response code="500">Server Error. Please, report administrator</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([Required] RegisterDto user)
        {
            var command = new RegisterUserCommand()
            {
                Login = user.Login,
                Password = user.Password,
            };
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Login([Required] LoginDto loginDto)
        {
            var command = Mapper.Map<CheckUserPasswordQuery>(loginDto);
            var user = await Mediator.Send(command);
            var token = tokenService.BuildToken(user);
            return Ok(token);
        }
    }
}
