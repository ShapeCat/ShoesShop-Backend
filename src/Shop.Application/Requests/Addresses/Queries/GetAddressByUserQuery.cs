using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Addresses.Queries
{
    public record GetAddressByUserQuery : IRequest<AddressVm>
    {
        public Guid UserId { get; set; }
    }

    public class GetAddressByUserValidator : AbstractValidator<GetAddressByUserQuery>
    {
        public GetAddressByUserValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    public class GetAddressByUserQueryHandler : AbstractQueryHandler<GetAddressByUserQuery, AddressVm>
    {
        public GetAddressByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<AddressVm> Handle(GetAddressByUserQuery request, CancellationToken cancellationToken)
        {
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            var addressRepository = UnitOfWork.GetRepositoryOf<Address>();

            var user = await userRepository.GetAsync(request.UserId, cancellationToken);
            if (user.AddressId is null) throw new NotFoundException(request.UserId.ToString(), typeof(Address));
            var address = await addressRepository.FindFirstAsync(x => x.AddressId == user.AddressId, cancellationToken);
            return Mapper.Map<AddressVm>(address);
        }
    }
}
