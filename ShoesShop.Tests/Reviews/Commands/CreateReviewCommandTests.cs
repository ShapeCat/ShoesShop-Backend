using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Reviews.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Reviews.Commands
{
    public class CreateReviewCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_CreateReview_WhenCorrect()
        {
            var command = new CreateReviewCommand()
            {
                ModelId = TestData.UpdateModelId,
                UserId = TestData.UpdateUserId,
                Comment = "add test comment",
                Rating = 4,
                PublishDate = DateTime.Now,
            };
            var handler = new CreateReviewCommandHandler(UnitOfWork);

            var createdReviewId = await handler.Handle(command, CancellationToken.None);

            DbContext.Reviews.SingleOrDefault(x => x.ReviewId == createdReviewId
                                                    && x.ModelId == command.ModelId
                                                    && x.UserId == command.UserId
                                                    && x.Comment == command.Comment
                                                    && x.Rating == command.Rating
                                                    && x.Rating == command.Rating).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotExists()
        {
            var command = new CreateReviewCommand()
            {
                ModelId = TestData.UpdateModelId,
                UserId = Guid.NewGuid(),
                Comment = "add test comment",
                Rating = 4,
                PublishDate = DateTime.Now,
            };
            var handler = new CreateReviewCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelNotExists()
        {
            var command = new CreateReviewCommand()
            {
                ModelId = Guid.NewGuid(),
                UserId = TestData.UpdateUserId,
                Comment = "add test comment",
                Rating = 4,
                PublishDate = DateTime.Now,
            };
            var handler = new CreateReviewCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
