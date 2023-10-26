using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Base
{
    public abstract class AbstractCommand
    {
        protected readonly IUnitOfWork unitOfWork;

        public AbstractCommand(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
    }
}
