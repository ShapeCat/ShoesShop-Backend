using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class UpdatePriceCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_ThrowException_WhenPriceNotExists()
        {
            //Arrange
            var priceToCreate = new Price()
            {
                BasePrice = 1,
                Id = Guid.NewGuid(),
            };
            var command = new UpdatePriceCommand()
            {
                PriceId = priceToCreate.Id,
                BasePrice = priceToCreate.BasePrice,
            };
            var handler = new UpdatePriceCommandHandler(UnitOfWork);

            //Act 
            //Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_CreatePrice_WhenPriceExists()
        {
            //Arrange
            var priceToCreate = new Price()
            {
                BasePrice = 1,
                Id = TestData.UpdatePriceId,
            };
            var command = new UpdatePriceCommand()
            {
                PriceId = priceToCreate.Id,
                BasePrice = priceToCreate.BasePrice,
            };
            var handler = new UpdatePriceCommandHandler(UnitOfWork);

            //Act 
            await handler.Handle(command, CancellationToken.None);

            //Assert
            await DbContext.Prices.SingleOrDefaultAsync(x => x.Id == priceToCreate.Id
                                                          && x.BasePrice == priceToCreate.BasePrice
                                                          && x.SaleEndDate == priceToCreate.SaleEndDate
                                                          && x.Sale == priceToCreate.Sale).ShouldNotBeNull();
        }
    }
}
