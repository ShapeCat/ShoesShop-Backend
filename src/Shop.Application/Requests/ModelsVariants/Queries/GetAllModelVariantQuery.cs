﻿using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Queries
{
    public record GetAllModelVariantQuery : IRequest<IEnumerable<ModelVariantVm>> { }

    public class GetAllModelVariantsQueryHander : AbstractQueryHandler<GetAllModelVariantQuery, IEnumerable<ModelVariantVm>>
    {
        public GetAllModelVariantsQueryHander(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ModelVariantVm>> Handle(GetAllModelVariantQuery request, CancellationToken cancellationToken)
        {
            var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
            var allModelVariants = await modelVariantRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<ModelVariantVm>>(allModelVariants);
        }
    }
}
