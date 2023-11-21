using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Commands
{
    public class CreateModelVariantSaleCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateSale_WhenCorrect()
        {
            var saleToCreate = new Sale()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
                Percent = 0.5f,
                SaleEndDate = DateTime.Now,
            };
            var command = new CreateModelVariantSaleCommand()
            {
                ModelVariantId = saleToCreate.ModelVariantId,
                Percent = saleToCreate.Percent,
                SaleEndDate = saleToCreate.SaleEndDate,
            };
            var handler = new CreateModelVariantSaleCommandHandler(UnitOfWork);

            var createdSaleId = await handler.Handle(command, CancellationToken.None);
            DbContext.Sales.SingleOrDefault(x => x.SaleId == createdSaleId
                                                 && x.Percent == saleToCreate.Percent
                                                 && x.SaleEndDate == saleToCreate.SaleEndDate
                                                 && x.ModelVariantId == saleToCreate.ModelVariantId).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelVariantNotExists()
        {
            var saleToCreate = new Sale()
            {
                ModelVariantId = Guid.NewGuid(),
                Percent = 0.5f,
                SaleEndDate = DateTime.Now,
            };
            var command = new CreateModelVariantSaleCommand()
            {
                ModelVariantId = saleToCreate.ModelVariantId,
                Percent = saleToCreate.Percent,
                SaleEndDate = saleToCreate.SaleEndDate,
            };
            var handler = new CreateModelVariantSaleCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
