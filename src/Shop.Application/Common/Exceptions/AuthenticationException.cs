namespace ShoesShop.Application.Common.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string login) : base($"Login error for {login}. Invalid password or username") { }
    }
}
