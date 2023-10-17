using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class CreateShoesCommandTests : CommandTestAbstract
    {
        private readonly IShoesRepository shoesRepository;

        public CreateShoesCommandTests() => shoesRepository = new ShoesRepository(dbContext);

        [Fact]
        public async Task Should_CreateShoes_WhenShoesNotExists()
        {
            // Arrange 
            var newShoesName = "CreateShoesName";
            var command = new CreateShoesCommand()
            {
                Name = newShoesName,
            };
            var handler = new CreateShoesCommandHandler(shoesRepository);

            // Act 
            var shoesId = await handler.Handle(command, CancellationToken.None);

            // Assert
            dbContext.Shoes.SingleOrDefault(x => x.Id == shoesId
                                              && x.Name == newShoesName).ShouldNotBeNull();
        }
    }
}
