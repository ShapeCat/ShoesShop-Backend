using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Commands
{
    public record CreateModelVariantSaleCommand : IRequest<Guid>
    {
        public Guid ModelVariantId { get; set; }
        public float Percent { get; set; }
        public DateTime SaleEndDate { get; set; }
    }

    public class CreateModelVariantSaleCommandValidator : AbstractValidator<CreateModelVariantSaleCommand>
    {
        public CreateModelVariantSaleCommandValidator()
        {
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
            RuleFor(x => x.Percent).GreaterThan(0.0f);
            RuleFor(x => x.SaleEndDate).GreaterThan(DateTime.Now);
        }
    }

    public class CreateModelVariantSaleCommandHandler : AbstractCommandHandler<CreateModelVariantSaleCommand, Guid>
    {
        public CreateModelVariantSaleCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelVariantSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var saleRepository = UnitOfWork.GetRepositoryOf<Sale>();
                var modelVarinatRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelVariant = await modelVarinatRepository.GetAsync(request.ModelVariantId, cancellationToken);
                var sale = new Sale()
                {
                    SaleId = Guid.NewGuid(),
                    ModelVariant = modelVariant,
                    Percent = request.Percent,
                    SaleEndDate = request.SaleEndDate,
                };
                await saleRepository.AddAsync(sale, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return sale.SaleId;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
