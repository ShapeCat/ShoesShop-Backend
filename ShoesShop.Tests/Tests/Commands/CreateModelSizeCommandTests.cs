using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class CreateModelSizeCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateModelSize_WhenModelSizeNotExists()
        {
            var modelSizeToCreate = new ModelSize()
            {
                Size = 100,
            };
            var command = new CreateModelSizeCommand()
            {
                Size = modelSizeToCreate.Size,
            };
            var handler = new CreateModelSizeCommandHandler(UnitOfWork);

            //Act 
            var createdModelSizeId = await handler.Handle(command, CancellationToken.None);

            //Assert
            DbContext.ModelsSizes.SingleOrDefault(x => x.ModelSizeId == createdModelSizeId
                                                    && x.Size == modelSizeToCreate.Size).ShouldNotBeNull();

        }

        [Fact]
        public async void Should_ThrowException_WhenSameModelSizeExists()
        {
            var modelSizeToCreate = new ModelSize()
            {
                Size = TestData.ExistedModelSize,
            };
            var command = new CreateModelSizeCommand()
            {
                Size = modelSizeToCreate.Size,
            };
            var handler = new CreateModelSizeCommandHandler(UnitOfWork);

            //Act
            //Assert
            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
