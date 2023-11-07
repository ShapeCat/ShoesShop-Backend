using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Base
{
    public abstract class AbstractCommandHandler
    {
        protected readonly IUnitOfWork unitOfWork;

        public AbstractCommandHandler(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
    }
}
