using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class UpdateModelCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void SHould_UpdateModel_WhenModeExists()
        {
            // Arrange
            var modelToUpdate = new Model()
            {
                Name = "update test name",
                Color = "update test color",
                Brend = "update test brend",
                SkuId = "update test skuid",
                ReleaseDate = DateTime.Now,
            };
            var command = new UpdateModelCommand()
            {
                ModelId = TestData.UpdateModelId,
                Name = modelToUpdate.Name,
                Color = modelToUpdate.Color,
                Brend = modelToUpdate.Brend,
                SkuId = modelToUpdate.SkuId,
                ReleaseDate = modelToUpdate.ReleaseDate,
            };
            var handler = new UpdateModelCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            DbContext.Models.SingleOrDefault(x => x.Id == TestData.UpdateModelId
                                                 && x.Name == modelToUpdate.Name
                                                 && x.Color == modelToUpdate.Color
                                                 && x.Brend == modelToUpdate.Brend
                                                 && x.SkuId == modelToUpdate.SkuId
                                                 && x.ReleaseDate == modelToUpdate.ReleaseDate).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            // Arrange
            var modelToUpdate = new Model()
            {
                Name = "update test name",
                Color = "update test color",
                Brend = "update test brend",
                SkuId = "update test skuid",
                ReleaseDate = DateTime.Now,
            };
            var command = new UpdateModelCommand()
            {
                ModelId = Guid.NewGuid(),
                Name = modelToUpdate.Name,
                Color = modelToUpdate.Color,
                Brend = modelToUpdate.Brend,
                SkuId = modelToUpdate.SkuId,
                ReleaseDate = modelToUpdate.ReleaseDate,
            };
            var handler = new UpdateModelCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }
    }
}
