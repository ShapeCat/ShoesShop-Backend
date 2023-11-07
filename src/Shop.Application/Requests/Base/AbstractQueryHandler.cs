using AutoMapper;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Base
{
    public abstract class AbstractQueryHandler
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public AbstractQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
