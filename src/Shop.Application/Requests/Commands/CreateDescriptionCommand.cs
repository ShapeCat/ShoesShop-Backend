using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateDescriptionCommand : IRequest<Guid>
    {
        public Guid ShoesId { get; set; }
        public string ColorName { get; set; }
        public string SkuID { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class CreateDescriptionCommandHandler : IRequestHandler<CreateDescriptionCommand, Guid>
    {
        private readonly IDescriptionRepository descriptionRepository;

        public CreateDescriptionCommandHandler(IDescriptionRepository descriptionRepository) => this.descriptionRepository = descriptionRepository;

        public async Task<Guid> Handle(CreateDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var description = new Description()
                {
                    ColorName = request.ColorName,
                    SkuID = request.SkuID,
                    ReleaseDate = request.ReleaseDate,
                };
                await descriptionRepository.AddAsync(request.ShoesId, description, cancellationToken);
                await descriptionRepository.SaveChangesAsync(cancellationToken);
                var output = await descriptionRepository.GetByShoesAsync(request.ShoesId, cancellationToken);
                return output.Id;
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
