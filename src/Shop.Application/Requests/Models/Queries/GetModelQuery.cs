using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.Queries
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
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var model = await modelRepository.GetAsync(request.ModelId, cancellationToken);
                return Mapper.Map<ModelVm>(model);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
