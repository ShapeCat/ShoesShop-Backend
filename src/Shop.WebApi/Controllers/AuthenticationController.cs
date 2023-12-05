﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Requests.Authentication.Commands;
using ShoesShop.Application.Requests.Authentication.OutputVMs;
using ShoesShop.Application.Requests.Authentication.Queries;
using ShoesShop.WebApi.Authentication;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class AuthenticationController : AbstractController
    {
        private readonly ITokenService tokenService;

        public AuthenticationController(IMapper mapper, ITokenService tokenService) : base(mapper) => this.tokenService = tokenService;

        [HttpPost("register")]
        [AllowAnonymous]
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
        public async Task<ActionResult> Login([Required] LoginDto loginDto)
        {
            AuthenticatedDataVm user;
            var command = Mapper.Map<CheckUserPasswordQuery>(loginDto);
            user = await Mediator.Send(command);
            var token = tokenService.BuildToken(user);
            return Ok(token);
        }
    }
}
