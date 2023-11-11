using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Base
{
    public abstract class AbstractQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public AbstractQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
