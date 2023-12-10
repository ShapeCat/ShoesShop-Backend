using ShoesShop.Application.Requests.Authentication.OutputVMs;

namespace ShoesShop.WebApi.Services.Authentication
{
    public interface ITokenService
    {
        string BuildToken(AuthenticatedDataVm user);
    }
}
