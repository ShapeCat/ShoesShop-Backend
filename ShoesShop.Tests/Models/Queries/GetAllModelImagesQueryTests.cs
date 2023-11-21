using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Models.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Queries
{
    public class GetAllModelImagesQueryTests : AbstractQueryTests
    {
        public GetAllModelImagesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnModelImages_WhenCorrect()
        {
            var query = new GetAllModelImagesQuery()
            {
                ModelId = TestData.UpdateModelId,
            };
            var handler = new GetAllModelImagesQueryHandler(UnitOfWork, Mapper);

            var modelImages = await handler.Handle(query, CancellationToken.None);

            modelImages.ShouldAllBe(x => x is ModelImageVm);
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelNotExists()
        {
            var query = new GetAllModelImagesQuery()
            {
                ModelId = Guid.NewGuid(),
            };
            var handler = new GetAllModelImagesQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
