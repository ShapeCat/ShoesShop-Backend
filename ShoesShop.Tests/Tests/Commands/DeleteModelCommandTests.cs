using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class DeleteModelCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteModel_WhenModelExists()
        {
            // Arrange
            var command = new DeleteModelCommand()
            {
                ModelId = TestData.DeleteModelId
            };
            var handler = new DeleteModelCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            DbContext.Models.FirstOrDefault(x => x.ModelId == TestData.DeleteModelId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelNotExists()
        {
            // Arrange
            var command = new DeleteModelCommand()
            {
                ModelId = Guid.NewGuid(),
            };
            var handler = new DeleteModelCommandHandler(UnitOfWork);

            // Act
            // Assert 
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
