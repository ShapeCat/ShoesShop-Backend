using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Authentication.OutputVMs;
using ShoesShop.Application.Requests.Authentication.Queries;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Authentication.Queries
{
    public class CheckUserPasswordQueryTests : AbstractQueryTests
    {
        public CheckUserPasswordQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnLoginUserData_WhenCorrect()
        {
            var query = new CheckUserPasswordQuery()
            {
                Login = TestData.ExistedLoginData.login,
                Password = TestData.ExistedLoginData.password,
            };
            var handler = new CheckUserPasswordCommandHandler(UnitOfWork, Mapper);

            var authenticatedUserData = await handler.Handle(query, CancellationToken.None);

            authenticatedUserData.ShouldBeOfType<AuthenticatedDataVm>();
            authenticatedUserData.Role.ShouldBe(Roles.User);
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotExists()
        {
            var query = new CheckUserPasswordQuery()
            {
                Login = "login test login",
                Password = "login test password",
            };
            var handler = new CheckUserPasswordCommandHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_WhenPasswordIncorrect()
        {
            var query = new CheckUserPasswordQuery()
            {
                Login = TestData.ExistedLoginData.login,
                Password = "login test password",
            };
            var handler = new CheckUserPasswordCommandHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<AuthenticationException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
