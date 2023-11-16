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

        protected IMapper Mapper { get; }
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected AbstractController(IMapper mapper) => Mapper = mapper;
    }
}

