using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetAdressQueryTests : AbstractQueryTests
    {
        public GetAdressQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAdress_WhenAdressExists()
        {
            // Arrange
            var query = new GetAdressQuery()
            {
                AdressId = TestData.UpdateAdressId,
            };
            var handler = new GetAdressQueryHandler(UnitOfWork, Mapper);

            // Act
            var adress = await handler.Handle(query, CancellationToken.None);

            // Assert
            adress.ShouldBeOfType<AdressVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAdressNotExists()
        {
            // Arrange
            var query = new GetAdressQuery()
            {
                AdressId = Guid.NewGuid(),
            };
            var handler = new GetAdressQueryHandler(UnitOfWork, Mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

    }
}
