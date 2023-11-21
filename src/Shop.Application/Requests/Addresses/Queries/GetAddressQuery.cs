using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Addresses.Queries
{
    public record GetAddressQuery : IRequest<AddressVm>
    {
        public Guid AddressId { get; set; }
    }

    public class GetAddressQueryValidator : AbstractValidator<GetAddressQuery>
    {
        public GetAddressQueryValidator()
        {
            RuleFor(x => x.AddressId).NotEqual(Guid.Empty);
        }
    }

    public class GetAddressQueryHandler : AbstractQueryHandler<GetAddressQuery, AddressVm>
    {
        public GetAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<AddressVm> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var addressRepository = UnitOfWork.GetRepositoryOf<Address>();
                var address = await addressRepository.GetAsync(request.AddressId, cancellationToken);
                return Mapper.Map<AddressVm>(address);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
