using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.Queries
{
    public record GetImagesByModelQuery : IRequest<IEnumerable<ImageVm>>
    {
        public Guid ModelId { get; set; }
    }

    public class GetImagesByModelQueryValidator : AbstractValidator<GetImagesByModelQuery>
    {
        public GetImagesByModelQueryValidator()
        {
            RuleFor(x => x.ModelId).NotEqual(Guid.Empty);
        }
    }

    public class GetImagesByModelQueryHandler : AbstractQueryHandler<GetImagesByModelQuery, IEnumerable<ImageVm>>
    {
        public GetImagesByModelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ImageVm>> Handle(GetImagesByModelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
                await modelRepository.GetAsync(request.ModelId, cancellationToken);
                var images = await imageRepository.FindAllAsync(x => x.ModelId == request.ModelId, cancellationToken);
                return Mapper.Map<IEnumerable<ImageVm>>(images);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
