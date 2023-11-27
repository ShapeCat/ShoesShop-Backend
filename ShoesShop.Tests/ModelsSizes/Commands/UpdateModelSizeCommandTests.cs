using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsSizes.Commands
{
    public class UpdateModelSizeCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_UpdateModelSize_WhenCorrect()
        {
            var command = new UpdateModelSizeCommand()
            {
                ModelSizeId = TestData.UpdateModelSizeId,
                Size = 100,
            };
            var handler = new UpdateModelSizeCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.ModelsSizes.SingleOrDefault(x => x.ModelSizeId == TestData.UpdateModelSizeId
                                                       && x.Size == command.Size).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelSizeNotExists()
        {
            var command = new UpdateModelSizeCommand()
            {
                ModelSizeId = Guid.NewGuid(),
                Size = 100,
            };
            var handler = new UpdateModelSizeCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenSameModelSizeExists()
        {
            var command = new UpdateModelSizeCommand()
            {
                ModelSizeId = Guid.NewGuid(),
                Size = TestData.ExistedModelSize,
            };
            var handler = new UpdateModelSizeCommandHandler(UnitOfWork);

            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
