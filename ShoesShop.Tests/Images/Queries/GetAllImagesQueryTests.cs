using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Images.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Queries
{
    public class GetAllImagesQueryTests : AbstractQueryTests
    {
        public GetAllImagesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAllImages()
        {
            // Arraange
            var query = new GetAllImagesQuery();
            var handler = new GetAllImagesQueryHadler(UnitOfWork, Mapper);

            // Act
            var allImages = await handler.Handle(query, CancellationToken.None);

            // Assert
            allImages.ShouldAllBe(x => x is ImageVm);
            allImages.Count().ShouldBe(2);
        }
    }
}
