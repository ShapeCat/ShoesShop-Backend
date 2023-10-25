using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record UpdateDescriptionCommand : IRequest<Unit>
    {
        public Guid DescriptionId { get; set; }
        public string ColorName { get; set; }
        public string SkuID { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class UpdateDescriptionCommandHandler : IRequestHandler<UpdateDescriptionCommand, Unit>
    {
        private readonly IDescriptionRepository descriptionRepository;

        public UpdateDescriptionCommandHandler(IDescriptionRepository descriptionRepository) => this.descriptionRepository = descriptionRepository;

        public async Task<Unit> Handle(UpdateDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newDescription = new Description()
                {
                    Id = request.DescriptionId,
                    ColorName = request.ColorName,
                    SkuID = request.SkuID,
                    ReleaseDate = request.ReleaseDate,
                };
                await descriptionRepository.EditAsync(newDescription, cancellationToken);
                await descriptionRepository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
