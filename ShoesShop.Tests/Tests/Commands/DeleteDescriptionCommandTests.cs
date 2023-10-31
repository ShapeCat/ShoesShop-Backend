//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Commands;
//using ShoesShop.Persistence.Repository;
//using ShoesShop.Tests.Core;
//using Shouldly;
//using Xunit;

//namespace ShoesShop.Tests.Tests.Commands
//{
//    public class DeleteDescriptionCommandTests : AbstractCommandTest
//    {
//        [Fact]
//        public async Task Should_DeleteDescriptions_WhenDescriptionExists()
//        {
//            // Arrange
//            var command = new DeleteDescriptionCommand()
//            {
//                DescriptionId = ShoesShopTestContext.DescriptionToDelete
//            };
//            var handler = new DeleteDescriptionCommandHandler(unitOfWork);

//            // Act
//            await handler.Handle(command, CancellationToken.None);

//            // Assert 
//            dbContext.Descriptions.FirstOrDefault(x => x.Id == ShoesShopTestContext.DescriptionToDelete).ShouldBeNull();
//        }

//        [Fact]
//        public async Task Should_ThrowException_WhenDescriptionNotExists()
//        {
//            // Arrange
//            var command = new DeleteDescriptionCommand()
//            {
//                DescriptionId = Guid.NewGuid(),
//            };
//            var handler = new DeleteDescriptionCommandHandler(unitOfWork);

//            // Act
//            // Assert
//            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
//        }
//    }
//}
