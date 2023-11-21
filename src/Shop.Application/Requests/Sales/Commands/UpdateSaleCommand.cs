using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Sales.Commands
{
    public record UpdateSaleCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }
        public float Percent { get; set; }
        public DateTime SaleEndDate { get; set; }
    }

    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.SaleId).NotEqual(Guid.Empty);
            RuleFor(x => x.Percent).GreaterThan(0.0f);
            RuleFor(x => x.SaleEndDate).GreaterThan(DateTime.Now);
        }
    }

    public class UpdateSaleCommandHandler : AbstractCommandHandler<UpdateSaleCommand, Unit>
    {
        public UpdateSaleCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var saleRepository = UnitOfWork.GetRepositoryOf<Sale>();
                var newSale = new Sale()
                {
                    SaleId = request.SaleId,
                    Percent = request.Percent,
                    SaleEndDate = request.SaleEndDate,
                };
                await saleRepository.EditAsync(newSale, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
