using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    [Produces("application/json")]
    public abstract class AbstractController : Controller
    {
        private IMediator mediator;
        protected readonly IMapper mapper;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected AbstractController(IMapper mapper) => this.mapper = mapper;
    }
}

