using MediatR;
using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.Application.Requests.Abstraction
{
    public abstract class AbstractCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected IUnitOfWork UnitOfWork { get; }

        public AbstractCommandHandler(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
