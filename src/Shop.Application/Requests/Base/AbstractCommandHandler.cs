using MediatR;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Base
{
    public abstract class AbstractCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IUnitOfWork unitOfWork;

        public AbstractCommandHandler(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
