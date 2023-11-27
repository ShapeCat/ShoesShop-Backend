using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsSizes.Commands
{
    public class CreateModelSizeCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateModelSize_WhenCorrect()
        {
            var command = new CreateModelSizeCommand()
            {
                Size = 100,
            };
            var handler = new CreateModelSizeCommandHandler(UnitOfWork);

            var createdModelSizeId = await handler.Handle(command, CancellationToken.None);

            DbContext.ModelsSizes.SingleOrDefault(x => x.ModelSizeId == createdModelSizeId
                                                    && x.Size == command.Size).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenSameModelSizeExists()
        {
            var command = new CreateModelSizeCommand()
            {
                Size = TestData.ExistedModelSize,
            };
            var handler = new CreateModelSizeCommandHandler(UnitOfWork);

            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
