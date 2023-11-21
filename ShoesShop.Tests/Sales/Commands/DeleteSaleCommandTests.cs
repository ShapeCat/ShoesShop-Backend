using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Sales.Commands
{
    public class DeleteSaleCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteSale_WhenCorrect()
        {
            var command = new DeleteSaleCommand()
            {
                SaleId = TestData.DeleteSaleId
            };
            var handler = new DeleteSaleCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Sales.FirstOrDefault(x => x.ModelVariantId == TestData.DeleteModelVariantId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenSaleNotExists()
        {
            var command = new DeleteSaleCommand()
            {
                SaleId = Guid.NewGuid(),
            };
            var handler = new DeleteSaleCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
