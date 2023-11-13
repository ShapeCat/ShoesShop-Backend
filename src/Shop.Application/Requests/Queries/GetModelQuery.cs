using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetModelQuery : IRequest<ModelVm>
    {
        public Guid ModelId { get; set; }
    }

    public class GetModelQueryHandler : AbstractQueryHandler<GetModelQuery, ModelVm>
    {
        public GetModelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ModelVm> Handle(GetModelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelRepository = unitOfWork.GetRepositoryOf<Model>();
                var model = await modelRepository.GetAsync(request.ModelId, cancellationToken);
                return mapper.Map<ModelVm>(model);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
