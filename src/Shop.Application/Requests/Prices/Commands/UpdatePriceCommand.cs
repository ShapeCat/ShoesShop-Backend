using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Prices.Commands
{
    public record UpdatePriceCommand : IRequest<Unit>
    {
        public Guid PriceId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? Sale { get; set; }
        public DateTime? SaleEndDate { get; set; }
    }

    public class UpdatePriceCommandHandler : AbstractCommandHandler<UpdatePriceCommand, Unit>
    {
        public UpdatePriceCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var priceRepository = UnitOfWork.GetRepositoryOf<Price>();
                var price = new Price()
                {
                    PriceId = request.PriceId,
                    BasePrice = request.BasePrice,
                    Sale = request.Sale,
                    SaleEndDate = request.SaleEndDate,
                };
                await priceRepository.EditAsync(price, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException)
            {
                throw;
            }
        }
    }
}
