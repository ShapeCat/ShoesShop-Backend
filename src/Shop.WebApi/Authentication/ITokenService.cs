using ShoesShop.Application.Requests.Authentication.OutputVMs;

namespace ShoesShop.WebApi.Authentication
{
    public interface ITokenService
    {
        string BuildToken(AuthenticatedDataVm user);
    }
}
