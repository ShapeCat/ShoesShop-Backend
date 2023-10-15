using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetDescriptionQuery : IRequest<DescriptionVm>
    {
        public Guid DescriptionId { get; set; }
    }
    public class GetDescriptionQueryHandler : IRequestHandler<GetDescriptionQuery, DescriptionVm>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly IMapper mapper;

        public GetDescriptionQueryHandler(IDescriptionRepository descriptionRepository, IMapper mapper)
        {
            this.descriptionRepository = descriptionRepository;
            this.mapper = mapper;
        }

        public async Task<DescriptionVm> Handle(GetDescriptionQuery request, CancellationToken cancellationToken)
        {
            var description = await descriptionRepository.GetAsync(request.DescriptionId, cancellationToken);
            return mapper.Map<DescriptionVm>(description);
        }
    }
}
