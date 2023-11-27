using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Commands
{
    public class CreateModelVariantCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateModelVariant_WhenCorrect()
        {
            var command = new CreateModelVariantCommand()
            {
                ModelId = TestData.UpdateModelId,
                ModelSizeId = TestData.UpdateModelSizeId,
                ItemsLeft = 4,
                Price = 1000,
            };
            var handler = new CreateModelVariantCommandHandler(UnitOfWork);

            var createdModelVariantId = await handler.Handle(command, CancellationToken.None);
            DbContext.ModelsVariants.SingleOrDefault(x => x.ModelVariantId == createdModelVariantId
                                                          && x.ModelId == command.ModelId
                                                          && x.ModelSizeId == command.ModelSizeId
                                                          && x.Price == command.Price).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelNotExists()
        {
            var command = new CreateModelVariantCommand()
            {
                ModelId = Guid.NewGuid(),
                ModelSizeId = TestData.UpdateModelSizeId,
                ItemsLeft = 4,
                Price = 1000,
            };
            var handler = new CreateModelVariantCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenModelSizeNotExists()
        {
            var command = new CreateModelVariantCommand()
            {
                ModelId = TestData.UpdateModelId,
                ModelSizeId = Guid.NewGuid(),
                ItemsLeft = 4,
                Price = 1000,
            };
            var handler = new CreateModelVariantCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
