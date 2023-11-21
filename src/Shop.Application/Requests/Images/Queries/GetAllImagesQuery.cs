using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Images.Queries
{
    public record GetAllImagesQuery : IRequest<IEnumerable<ImageVm>> { }

    public class GetAllImagesQueryHandler : AbstractQueryHandler<GetAllImagesQuery, IEnumerable<ImageVm>>
    {
        public GetAllImagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ImageVm>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
            var Images = await imageRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<ImageVm>>(Images);
        }
    }
}
