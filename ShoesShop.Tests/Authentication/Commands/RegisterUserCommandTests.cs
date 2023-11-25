using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Authentication.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Authentication.Commands
{
    public class RegisterUserCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_RegisterNewUser_WhenCorrect()
        {
            var command = new RegisterUserCommand()
            {
                Login = "register test login",
                Password = "register test password"
            };
            var handler = new RegisterUserCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);
            DbContext.Users.SingleOrDefault(x => x.Login == command.Login
                                                 && x.Password == User.HashPassword(command.Password)).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenUserWithSameLoginExists()
        {
            var command = new RegisterUserCommand()
            {
                Login = TestData.ExistedLoginData.login,
                Password = "register test password"
            };
            var handler = new RegisterUserCommandHandler(UnitOfWork);

            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
