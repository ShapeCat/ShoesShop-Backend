using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Prices.Commands
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
                PriceId = Guid.NewGuid(),
            };
            var command = new UpdatePriceCommand()
            {
                PriceId = priceToCreate.PriceId,
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
                PriceId = TestData.UpdatePriceId,
            };
            var command = new UpdatePriceCommand()
            {
                PriceId = priceToCreate.PriceId,
                BasePrice = priceToCreate.BasePrice,
            };
            var handler = new UpdatePriceCommandHandler(UnitOfWork);

            //Act 
            await handler.Handle(command, CancellationToken.None);

            //Assert
            await DbContext.Prices.SingleOrDefaultAsync(x => x.PriceId == priceToCreate.PriceId
                                                          && x.BasePrice == priceToCreate.BasePrice
                                                          && x.SaleEndDate == priceToCreate.SaleEndDate
                                                          && x.Sale == priceToCreate.Sale).ShouldNotBeNull();
        }
    }
}
