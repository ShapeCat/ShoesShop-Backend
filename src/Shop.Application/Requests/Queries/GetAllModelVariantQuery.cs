using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllModelVariantQuery : IRequest<IEnumerable<ModelVariantVm>> { }

    public class GetAllModelVariantsQueryHander : AbstractQueryHandler<GetAllModelVariantQuery, IEnumerable<ModelVariantVm>>
    {
        public GetAllModelVariantsQueryHander(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ModelVariantVm>> Handle(GetAllModelVariantQuery request, CancellationToken cancellationToken)
        {
            var modelVariantRepository = unitOfWork.GetRepositoryOf<ModelVariant>();
            var allModelVariants = await modelVariantRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ModelVariantVm>>(allModelVariants);
        }
    }
}
