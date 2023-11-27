using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Application.Requests.Users.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Users.Queries
{
    public class GetUserQueryTests : AbstractQueryTests
    {
        public GetUserQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnUser_WhenCorrect()
        {
            var query = new GetUserQuery()
            {
                UserId = TestData.UpdateUserId,
            };
            var handler = new GetUserQueryHandler(UnitOfWork, Mapper);

            var user = await handler.Handle(query, CancellationToken.None);

            user.ShouldBeOfType<UserVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotExists()
        {
            var query = new GetUserQuery()
            {
                UserId = Guid.NewGuid(),
            };
            var handler = new GetUserQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
