using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Addresses.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Queries
{
    public class GetAddressByUserQueryTests : AbstractQueryTests
    {
        public GetAddressByUserQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAddress_WhenCorrect()
        {
            var query = new GetAddressByUserQuery()
            {
                UserId = TestData.UpdateUserId,
            };
            var handler = new GetAddressByUserQueryHandler(UnitOfWork, Mapper);

            var address = await handler.Handle(query, CancellationToken.None);

            address.ShouldBeOfType<AddressVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAdressNotExists()
        {
            var query = new GetAddressByUserQuery()
            {
                UserId = Guid.NewGuid(),
            };
            var handler = new GetAddressByUserQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotExists()
        {
            var query = new GetAddressByUserQuery()
            {
                UserId = TestData.UserIdWithoutAddress,
            };
            var handler = new GetAddressByUserQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
