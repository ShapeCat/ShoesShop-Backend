using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Images.Queries
{
    public record GetImageQuery : IRequest<ImageVm>
    {
        public Guid ImageId { get; set; }
    }

    public class GetImageQueryHandler : AbstractQueryHandler<GetImageQuery, ImageVm>
    {
        public GetImageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ImageVm> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = UnitOfWork.GetRepositoryOf<Image>(true);
                var image = await imageRepository.GetAsync(request.ImageId, cancellationToken);
                return Mapper.Map<ImageVm>(image);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
