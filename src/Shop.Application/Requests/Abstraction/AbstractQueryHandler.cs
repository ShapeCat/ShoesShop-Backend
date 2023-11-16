using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.Application.Requests.Abstraction
{
    public abstract class AbstractQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }

        public AbstractQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) => (UnitOfWork, Mapper) = (unitOfWork, mapper);

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
