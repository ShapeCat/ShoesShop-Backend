//using AutoMapper;
//using MediatR;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Application.Requests.Queries.OutputVMs;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Queries
//{
//    public record GetShoesSizesByShoesQuery : IRequest<IEnumerable<ShoesSizeVm>>
//    {
//        public Guid ShoesId { get; set; }
//    }

//    public class GetShoesSizesByShoesQueryHandler : AbstractQuery, IRequestHandler<GetShoesSizesByShoesQuery, IEnumerable<ShoesSizeVm>>
//    {
//        public GetShoesSizesByShoesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

//        public async Task<IEnumerable<ShoesSizeVm>> Handle(GetShoesSizesByShoesQuery request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var shoesSizesRepository = unitOfWork.GetRepositoryOf<ModelSize>(true);
//                var shoesRepository = unitOfWork.GetRepositoryOf<Model>(true);

//                var parentShoes = await shoesRepository.FindAllAsync(x => x.Id == request.ShoesId, cancellationToken);
//                if (parentShoes.Count() == 0) throw new NotFoundException(request.ShoesId.ToString(), typeof(Model));
//                var shoesSize = await shoesSizesRepository.FindAllAsync(x => x.ShoesId == request.ShoesId, cancellationToken);
//                return mapper.Map<IEnumerable<ShoesSizeVm>>(shoesSize);
//            }
//            catch (NotFoundException ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
