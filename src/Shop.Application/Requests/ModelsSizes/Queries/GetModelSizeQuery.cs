using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsSizes.Queries
{
    public record GetModelSizeQuery : IRequest<ModelSizeVm>
    {
        public Guid ModelSizeId { get; set; }
    }

    public class GetModelSizeQueryValidator : AbstractValidator<GetModelSizeQuery>
    {
        public GetModelSizeQueryValidator()
        {
            RuleFor(x => x.ModelSizeId).NotEqual(Guid.Empty);
        }
    }

    public class GetModelSizeQueryHandler : AbstractQueryHandler<GetModelSizeQuery, ModelSizeVm>
    {
        public GetModelSizeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ModelSizeVm> Handle(GetModelSizeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                var modelSize = await modelSizeRepository.GetAsync(request.ModelSizeId, cancellationToken);
                return Mapper.Map<ModelSizeVm>(modelSize);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
