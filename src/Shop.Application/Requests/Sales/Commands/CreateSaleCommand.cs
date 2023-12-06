using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Sales.Commands
{
    public record CreateSaleCommand : IRequest<Guid>
    {
        public Guid ModelVariantId { get; set; }
        public float Percent { get; set; }
        public DateTime SaleEndDate { get; set; }
    }

    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
            RuleFor(x => x.Percent).InclusiveBetween(0f, 0.99f);
            RuleFor(x => x.SaleEndDate).GreaterThan(DateTime.Now);
        }
    }

    public class CreateSaleCommandHandler : AbstractCommandHandler<CreateSaleCommand, Guid>
    {
        public CreateSaleCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var saleRepository = UnitOfWork.GetRepositoryOf<Sale>();
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelVariant = await modelVariantRepository.GetAsync(request.ModelVariantId, cancellationToken);
                var sale = new Sale(modelVariant.ModelVariantId, request.Percent, request.SaleEndDate);
                await saleRepository.AddAsync(sale, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return sale.SaleId;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
