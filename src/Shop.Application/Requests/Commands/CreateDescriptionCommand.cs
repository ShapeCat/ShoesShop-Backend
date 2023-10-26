using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
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

    public class CreateDescriptionCommandHandler : AbstractCommand, IRequestHandler<CreateDescriptionCommand, Guid>
    {
        public CreateDescriptionCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Guid> Handle(CreateDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var descriptionRepository = unitOfWork.GetRepositoryOf<Description>(true);
                var description = new Description()
                {
                    ShoesId = request.ShoesId,
                    ColorName = request.ColorName,
                    SkuID = request.SkuID,
                    ReleaseDate = request.ReleaseDate,
                };

                await descriptionRepository.AddAsync(description, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                var output = await descriptionRepository.FindAllAsync(x => x.ShoesId == request.ShoesId, cancellationToken);
                return output.First().Id;
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
