using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllDescriptionsQuery : IRequest<IEnumerable<DescriptionVm>> { }

    public class GetAllDescriptionsQueryHandler : IRequestHandler<GetAllDescriptionsQuery, IEnumerable<DescriptionVm>>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly IMapper mapper;

        public GetAllDescriptionsQueryHandler(IDescriptionRepository descriptionRepository, IMapper mapper)
        {
            this.descriptionRepository = descriptionRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DescriptionVm>> Handle(GetAllDescriptionsQuery request, CancellationToken cancellationToken)
        {
            var descriptions = await descriptionRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<DescriptionVm>>(descriptions);
        }
    }
}
