using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Commands
{
    public class CreateModelVariantCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateModelVariant_WhenModelVariantNotExists()
        {
            var modelToCreate = new ModelVariant()
            {
                ModelId = TestData.UpdateModelId,
                ModelSizeId = TestData.UpdateModelSizeId,
                ItemsLeft = 4
            };
            var command = new CreateModelVariantCommand()
            {
                ModelId = modelToCreate.ModelId,
                ModelSizeId = modelToCreate.ModelSizeId,
                ItemsLeft = modelToCreate.ItemsLeft,
            };
            var handler = new CreateModelVariantCommandHandler(UnitOfWork);

            var createdModelVariantId = await handler.Handle(command, CancellationToken.None);
            DbContext.ModelsVariants.SingleOrDefault(x => x.ModelVariantId == createdModelVariantId
                                                          && x.ModelId == modelToCreate.ModelId
                                                          && x.ModelSizeId == modelToCreate.ModelSizeId).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelNotExists()
        {
            var modelToCreate = new ModelVariant()
            {
                ModelId = Guid.NewGuid(),
                ModelSizeId = TestData.UpdateModelSizeId,
                ItemsLeft = 4
            };
            var command = new CreateModelVariantCommand()
            {
                ModelId = modelToCreate.ModelId,
                ModelSizeId = modelToCreate.ModelSizeId,
                ItemsLeft = modelToCreate.ItemsLeft,
            };
            var handler = new CreateModelVariantCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenModelSizeNotExists()
        {
            var modelToCreate = new ModelVariant()
            {
                ModelId = TestData.UpdateModelId,
                ModelSizeId = Guid.NewGuid(),
                ItemsLeft = 4
            };
            var command = new CreateModelVariantCommand()
            {
                ModelId = modelToCreate.ModelId,
                ModelSizeId = modelToCreate.ModelSizeId,
                ItemsLeft = modelToCreate.ItemsLeft,
            };
            var handler = new CreateModelVariantCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
