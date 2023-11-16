using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllAddressesQuery : IRequest<IEnumerable<AddressVm>> { }

    public class GetAllAddressesQueryHander : AbstractQueryHandler<GetAllAddressesQuery, IEnumerable<AddressVm>>
    {
        public GetAllAddressesQueryHander(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<AddressVm>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            var addressRepository = UnitOfWork.GetRepositoryOf<Address>(true);
            var addresses = await addressRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<AddressVm>>(addresses);
        }
    }
}
