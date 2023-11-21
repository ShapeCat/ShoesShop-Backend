using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Images.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Queries
{
    public class GetImageQueryTests : AbstractQueryTests
    {
        public GetImageQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnImage_WhenCorrect()
        {
            var query = new GetImageQuery()
            {
                ImageId = TestData.UpdateImageId,
            };
            var handler = new GetImageQueryHandler(UnitOfWork, Mapper);

            var image = await handler.Handle(query, CancellationToken.None);

            image.ShouldBeOfType<ImageVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            var query = new GetImageQuery()
            {
                ImageId = Guid.NewGuid(),
            };
            var handler = new GetImageQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
