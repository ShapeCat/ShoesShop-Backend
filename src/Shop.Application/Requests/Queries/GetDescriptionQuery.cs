using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetDescriptionQuery : IRequest<DescriptionVm>
    {
        public Guid DescriptionId { get; set; }
    }
    public class GetDescriptionQueryHandler : AbstractQuery, IRequestHandler<GetDescriptionQuery, DescriptionVm>
    {
        public GetDescriptionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<DescriptionVm> Handle(GetDescriptionQuery request, CancellationToken cancellationToken)
        {
            var descriptionRepository = unitOfWork.GetRepositoryOf<Description>(true);
            var description = await descriptionRepository.GetAsync(request.DescriptionId, cancellationToken);
            return mapper.Map<DescriptionVm>(description);
        }
    }
}
