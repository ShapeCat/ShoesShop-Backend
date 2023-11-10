using AutoMapper;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class UserController : AbstractController
    {
        public UserController(IMapper mapper) : base(mapper) { }
    }
}
