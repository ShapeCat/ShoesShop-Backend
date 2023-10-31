//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Commands;
//using ShoesShop.Persistence.Repository;
//using ShoesShop.Tests.Core;
//using Shouldly;
//using Xunit;

//namespace ShoesShop.Tests.Tests.Commands
//{
//    public class DeleteShoesSizeCommandTests : AbstractCommandTest
//    {
//        [Fact]
//        public async Task Should_DeleteShoesSize_WhenShoesSizeExists()
//        {
//            // Arrange
//            var command = new DeleteShoesSizeCommand()
//            {
//                ShoesSizeId = ShoesShopTestContext.ShoesSizeToDelete,
//            };
//            var handler = new DeleteShoesSizeCommandHandler(unitOfWork);

//            // Act
//            await handler.Handle(command, CancellationToken.None);

//            // Assert 
//            dbContext.Sizes.SingleOrDefault(x => x.Id == ShoesShopTestContext.ShoesSizeToDelete).ShouldBeNull();
//        }

//        [Fact]
//        public async Task Should_ThrowException_WhenShoesSizeNotExists()
//        {
//            // Arrange
//            var command = new DeleteShoesSizeCommand()
//            {
//                ShoesSizeId = Guid.NewGuid(),
//            };
//            var handler = new DeleteShoesSizeCommandHandler(unitOfWork);

//            // Act
//            // Assert
//            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
//        }
//    }
//}
