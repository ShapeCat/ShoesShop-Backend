using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllImagesQuery : IRequest<IEnumerable<ImageVm>> { }

    public class GetAllImagesQueryHadler : AbstractQueryHandler<GetAllImagesQuery, IEnumerable<ImageVm>>
    {
        public GetAllImagesQueryHadler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ImageVm>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var imageRepository = UnitOfWork.GetRepositoryOf<Image>(true);
            var Images = await imageRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<ImageVm>>(Images);
        }
    }
}
