using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Reviews.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Reviews.Commands
{
    public class DeleteReviewCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteReview_WhenCorrect()
        {
            var command = new DeleteReviewCommand()
            {
                ReviewId = TestData.DeleteReviewId
            };
            var handler = new DeleteReviewCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Reviews.FirstOrDefault(x => x.ReviewId == command.ReviewId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenSaleNotExists()
        {
            var command = new DeleteReviewCommand()
            {
                ReviewId = Guid.NewGuid(),
            };
            var handler = new DeleteReviewCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
