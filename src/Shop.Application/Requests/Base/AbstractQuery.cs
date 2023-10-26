using AutoMapper;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Base
{
    public abstract class AbstractQuery
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public AbstractQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
