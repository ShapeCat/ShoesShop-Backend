using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Application.Requests.Users.Queries;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Users.Queries
{
    public class GetUsersByRoleQueryTests : AbstractQueryTests
    {
        public GetUsersByRoleQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnUsersWithGivenRole_WhenCorrect()
        {
            var query = new GetUsersByRoleQuery()
            {
                Role = Roles.User,
            };
            var handler = new GetUserByRoleQueryHandler(UnitOfWork, Mapper);

            var users = await handler.Handle(query, CancellationToken.None);

            users.ShouldAllBe(x => x is UserVm && x.Role == query.Role);
        }
    }
}
