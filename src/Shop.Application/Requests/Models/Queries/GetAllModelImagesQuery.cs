using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.Queries
{
    public record GetAllModelImagesQuery : IRequest<IEnumerable<ModelImageVm>>
    {
        public Guid ModelId { get; set; }
    }

    public class GetAllModelImagesQueryHandler : AbstractQueryHandler<GetAllModelImagesQuery, IEnumerable<ModelImageVm>>
    {
        public GetAllModelImagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ModelImageVm>> Handle(GetAllModelImagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                await modelRepository.GetAsync(request.ModelId, cancellationToken);
            }
            catch (NotFoundException) { throw; }

            var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
            var images = await imageRepository.FindAllAsync(x => x.ModelId == request.ModelId, cancellationToken);
            return Mapper.Map<IEnumerable<ModelImageVm>>(images);
        }
    }
}
