using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.Commands
{
    public record CreateModelImageCommand : IRequest<Guid>
    {
        public Guid ModelId { get; set; }
        public string Url { get; set; }
        public bool IsPreview { get; set; }
    }

    public class CreateModelImageCommandHandler : AbstractCommandHandler<CreateModelImageCommand, Guid>
    {
        public CreateModelImageCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async override Task<Guid> Handle(CreateModelImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var model = await modelRepository.GetAsync(request.ModelId, cancellationToken);
                var image = new Image()
                {
                    ImageId = Guid.NewGuid(),
                    IsPreview = request.IsPreview,
                    Url = request.Url,
                    Model = model,
                };

                await imageRepository.AddAsync(image, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return image.ImageId;

            }
            catch (NotFoundException) { throw; }
        }
    }
}
