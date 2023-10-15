using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetDescriptionByShoesQuery : IRequest<DescriptionVm>
    {
        public Guid ShoesId { get; set; }
    }

    public class GetDescriptionByShoesQueryHandler : IRequestHandler<GetDescriptionByShoesQuery, DescriptionVm>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly IMapper mapper;

        public GetDescriptionByShoesQueryHandler(IDescriptionRepository descriptionRepository, IMapper mapper)
        {
            this.descriptionRepository = descriptionRepository;
            this.mapper = mapper;
        }

        public async Task<DescriptionVm> Handle(GetDescriptionByShoesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var description = await descriptionRepository.GetByShoesAsync(request.ShoesId, cancellationToken);
                return mapper.Map<DescriptionVm>(description);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
