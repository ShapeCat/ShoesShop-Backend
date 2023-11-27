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
            var command = new UpdateSaleCommand()
            {
                SaleId = Guid.NewGuid(),
                Percent = 0.1f,
                SaleEndDate = DateTime.UtcNow,
            };
            var handler = new UpdateSaleCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_UpdateSale_WhenCorrect()
        {
            var command = new UpdateSaleCommand()
            {
                SaleId = TestData.UpdateSaleId,
                Percent = 0.1f,
                SaleEndDate = DateTime.UtcNow,
            };
            var handler = new UpdateSaleCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            await DbContext.Sales.SingleOrDefaultAsync(x => x.SaleId == command.SaleId
                                                          && x.Percent == command.Percent
                                                          && x.SaleEndDate == command.SaleEndDate).ShouldNotBeNull();
        }
    }
}
