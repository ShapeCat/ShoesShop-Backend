using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.WebApi.Dto;
using ShoesShop.WebAPI.Controllers;

namespace ShoesShop.WebApi.Controllers
{
    public class SaleController : AbstractController
    {
        public SaleController(IMapper mapper) : base(mapper) { }

    }
}
