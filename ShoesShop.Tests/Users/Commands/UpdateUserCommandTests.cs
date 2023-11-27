using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Users.Command;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Users.Commands
{
    public class UpdateUserCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_UpdateUser_WhenCorrect()
        {
            var command = new UpdateUserCommand()
            {
                UserId = TestData.UpdateUserId,
                AddressId = TestData.DeleteAddressId,
                UserName = "Update test username",
                Phone = "Update test phone"
            };
            var handler = new UpdateUserCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Users.SingleOrDefault(x => x.UserId == command.UserId
                                                 && x.AddressId == command.AddressId
                                                 && x.UserName == command.UserName
                                                 && x.Phone == command.Phone).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenUserNotExists()
        {
            var command = new UpdateUserCommand()
            {
                UserId = Guid.NewGuid(),
                AddressId = TestData.DeleteAddressId,
                UserName = "Update test username",
                Phone = "Update test phone"
            };
            var handler = new UpdateUserCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenGivenAddressNotExists()
        {
            var command = new UpdateUserCommand()
            {
                UserId = TestData.UpdateUserId,
                AddressId = Guid.NewGuid(),
                UserName = "Update test username",
                Phone = "Update test phone"
            };
            var handler = new UpdateUserCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
