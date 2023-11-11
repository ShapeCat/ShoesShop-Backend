using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateImageCommand : IRequest<Guid>
    {
        public string Url { get; set; }
        public bool IsPreview { get; set; }
    }

    public class CreateImageCommandHandler : AbstractCommandHandler<CreateImageCommand, Guid>
    {
        public CreateImageCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var imageRepository = unitOfWork.GetRepositoryOf<Image>(true);
            var imageToAdd = new Image()
            {
                Url = request.Url,
                IsPreview = request.IsPreview
            };

            await imageRepository.AddAsync(imageToAdd, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var createdImage = await imageRepository.FindAllAsync(x => x.Url == imageToAdd.Url
                                                                       && x.IsPreview == imageToAdd.IsPreview, cancellationToken);
            return createdImage.First().Id;
        }
    }
}
