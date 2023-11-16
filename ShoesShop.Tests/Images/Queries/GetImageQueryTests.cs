using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Queries
{
    public class GetImageQueryTests : AbstractQueryTests
    {
        public GetImageQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnImage_WhenImageExists()
        {
            // Arrange
            var query = new GetImageQuery()
            {
                ImageId = TestData.UpdateImageId,
            };
            var handler = new GetImageQueryHandler(UnitOfWork, Mapper);

            // Act
            var image = await handler.Handle(query, CancellationToken.None);

            // Assert
            image.ShouldBeOfType<ImageVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            // Arrange
            var query = new GetImageQuery()
            {
                ImageId = Guid.NewGuid(),
            };
            var handler = new GetImageQueryHandler(UnitOfWork, Mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
