using ShoesShop.Application.Requests.Authentication.OutputVMs;

namespace ShoesShop.WebApi.Services.TokenService
{
    public interface ITokenService
    {
        string BuildToken(AuthenticatedDataVm user);
    }
}
