using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ShoesShop.Application.Requests.Authentication.OutputVMs;

namespace ShoesShop.WebApi.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly string key;
        private readonly string issuer;
        private readonly string audience;
        private readonly TimeSpan ExpireDuration = new TimeSpan(0, 30, 0);

        public TokenService(string key, string issuer, string audience)
            => (this.key, this.issuer, this.audience) = (key, issuer, audience);

        public string BuildToken(AuthenticatedUserData user)
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(ClaimTypes.Name, user.UserName)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescription = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.Add(ExpireDuration), signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
        }
    }
}
