//using AutoMapper;
//using MediatR;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Application.Requests.Queries.OutputVMs;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Queries
//{
//    public record GetAllDescriptionsQuery : IRequest<IEnumerable<DescriptionVm>> { }

//    public class GetAllDescriptionsQueryHandler : AbstractQuery, IRequestHandler<GetAllDescriptionsQuery, IEnumerable<DescriptionVm>>
//    {
//        public GetAllDescriptionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

//        public async Task<IEnumerable<DescriptionVm>> Handle(GetAllDescriptionsQuery request, CancellationToken cancellationToken)
//        {
//            var descriptionRepository = unitOfWork.GetRepositoryOf<Description>(true);
//            var descriptions = await descriptionRepository.GetAllAsync(cancellationToken);
//            return mapper.Map<IEnumerable<DescriptionVm>>(descriptions);
//        }
//    }
//}
