using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Commands
{
    public class DeleteModelCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteModel_WhenCorrect()
        {
            var command = new DeleteModelCommand()
            {
                ModelId = TestData.DeleteModelId
            };
            var handler = new DeleteModelCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Models.FirstOrDefault(x => x.ModelId == TestData.DeleteModelId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelNotExists()
        {
            var command = new DeleteModelCommand()
            {
                ModelId = Guid.NewGuid(),
            };
            var handler = new DeleteModelCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
