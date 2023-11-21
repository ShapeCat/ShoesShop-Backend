using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Sales.Commands
{
    public class UpdateSaleCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_ThrowException_WhenSaleNotExists()
        {
            //Arrange
            var saleToUpdate = new Sale()
            {
                SaleId = Guid.NewGuid(),
                Percent = 0.1f,
                SaleEndDate = DateTime.UtcNow,
            };
            var command = new UpdateSaleCommand()
            {
                SaleId = saleToUpdate.SaleId,
                Percent = saleToUpdate.Percent,
                SaleEndDate = saleToUpdate.SaleEndDate,
            };
            var handler = new UpdateSaleCommandHandler(UnitOfWork);

            //Act 
            //Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_UpdateSale_WhenSaleExists()
        {
            //Arrange
            var saleToUpdate = new Sale()
            {
                SaleId = TestData.UpdateSaleId,
                Percent = 0.1f,
                SaleEndDate = DateTime.UtcNow,
            };
            var command = new UpdateSaleCommand()
            {
                SaleId = saleToUpdate.SaleId,
                Percent = saleToUpdate.Percent,
                SaleEndDate = saleToUpdate.SaleEndDate,
            };
            var handler = new UpdateSaleCommandHandler(UnitOfWork);

            //Act 
            await handler.Handle(command, CancellationToken.None);

            //Assert
            await DbContext.Sales.SingleOrDefaultAsync(x => x.SaleId == saleToUpdate.SaleId
                                                          && x.Percent == saleToUpdate.Percent
                                                          && x.SaleEndDate == saleToUpdate.SaleEndDate).ShouldNotBeNull();
        }
    }
}
