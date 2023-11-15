using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllAdressesQuery : IRequest<IEnumerable<AdressVm>> { }

    public class GetAllAdressesQueryHander : AbstractQueryHandler<GetAllAdressesQuery, IEnumerable<AdressVm>>
    {
        public GetAllAdressesQueryHander(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<AdressVm>> Handle(GetAllAdressesQuery request, CancellationToken cancellationToken)
        {
            var adressRepository = UnitOfWork.GetRepositoryOf<Adress>(true);
            var adresses = await adressRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<AdressVm>>(adresses);
        }
    }
}
