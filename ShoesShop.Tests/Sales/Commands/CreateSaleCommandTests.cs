﻿using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Commands
{
    public class CreateSaleCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateSale_WhenCorrect()
        {
            var command = new CreateSaleCommand()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
                Percent = 0.5f,
                SaleEndDate = DateTime.Now,
            };
            var handler = new CreateSaleCommandHandler(UnitOfWork);

            var createdSaleId = await handler.Handle(command, CancellationToken.None);
            DbContext.Sales.SingleOrDefault(x => x.SaleId == createdSaleId
                                                 && x.Percent == command.Percent
                                                 && x.SaleEndDate == command.SaleEndDate
                                                 && x.ModelVariantId == command.ModelVariantId).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new CreateSaleCommand()
            {
                ModelVariantId = Guid.NewGuid(),
                Percent = 0.5f,
                SaleEndDate = DateTime.Now,
            };
            var handler = new CreateSaleCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
