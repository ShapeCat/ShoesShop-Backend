using System.Security.Claims;
using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.WebApi.Services.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor contextAccessor;

        public Guid UserId
        {
            get
            {
                var id = contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }

        public CurrentUserService(IHttpContextAccessor contextAccessor) => this.contextAccessor = contextAccessor;
    }
}
