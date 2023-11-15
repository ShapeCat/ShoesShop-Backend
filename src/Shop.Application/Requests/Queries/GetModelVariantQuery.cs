using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetModelVariantQuery : IRequest<ModelVariantVm>
    {
        public Guid ModelVariantId { get; set; }
    }

    public class GetModelVariantQueryHandler : AbstractQueryHandler<GetModelVariantQuery, ModelVariantVm>
    {
        public GetModelVariantQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ModelVariantVm> Handle(GetModelVariantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelVariant = await modelVariantRepository.GetAsync(request.ModelVariantId, cancellationToken);
                return Mapper.Map<ModelVariantVm>(modelVariant);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
