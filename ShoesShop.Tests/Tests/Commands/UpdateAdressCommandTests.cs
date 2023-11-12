using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class UpdateAdressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_UpdateAdress_WhenAdressExists()
        {
            // Arrange
            var newAdress = new Adress()
            {
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "edit test house",
                Room = 1,
            };
            var command = new UpdateAdressCommand()
            {
                AdressId = TestData.UpdateAdressId,
                Country = newAdress.Country,
                City = newAdress.City,
                Street = newAdress.Street,
                House = newAdress.House,
                Room = newAdress.Room,
            };
            var handler = new UpdateAdressCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            DbContext.Adresses.FirstOrDefault(x => x.Id == TestData.UpdateAdressId
                                                   && x.Country == newAdress.Country
                                                   && x.City == newAdress.City
                                                   && x.Street == newAdress.Street
                                                   && x.House == newAdress.House
                                                   && x.Room == newAdress.Room).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAdressNotExists()
        {
            // Arrange
            var newAdress = new Adress()
            {
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "edit test house",
                Room = 1,
            };
            var command = new UpdateAdressCommand()
            {
                AdressId = Guid.NewGuid(),
                Country = newAdress.Country,
                City = newAdress.City,
                Street = newAdress.Street,
                House = newAdress.House,
                Room = newAdress.Room,
            };
            var handler = new UpdateAdressCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
