﻿
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class DeleteShoesCommandTests : CommandTestAbstract
    {
        private readonly IShoesRepository shoesRepository;

        public DeleteShoesCommandTests() => shoesRepository = new ShoesRepository(dbContext);

        [Fact]
        public async Task Should_DeleteShoes_WhenShoesExists()
        {
            // Arrange
            var command = new DeleteShoesCommand()
            {
                ShoesId = ShoesShopTextContext.ShoesToDelete
            };
            var handler = new DeleteShoesCommandHandler(shoesRepository);

            // Act 
            await handler.Handle(command, CancellationToken.None);

            // Assert
            dbContext.Shoes.SingleOrDefault(x => x.Id == ShoesShopTextContext.ShoesToDelete).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenShoesNotExists()
        {
            // Arrange 
            var command = new DeleteShoesCommand()
            {
                ShoesId = Guid.NewGuid()
            };
            var handler = new DeleteShoesCommandHandler(shoesRepository);

            // Act 
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

