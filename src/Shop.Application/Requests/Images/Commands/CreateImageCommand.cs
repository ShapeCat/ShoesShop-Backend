using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Images.Commands
{
    public record CreateImageCommand : IRequest<Guid>
    {
        public Guid ModelId { get; set; }
        public string Url { get; set; }
        public bool IsPreview { get; set; }
    }

    public class CreateModelImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        public CreateModelImageCommandValidator()
        {
            RuleFor(x => x.ModelId).NotEqual(Guid.Empty);
            RuleFor(x => x.Url).NotEmpty().MaximumLength(256);
        }
    }

    public class CreateModelImageCommandHandler : AbstractCommandHandler<CreateImageCommand, Guid>
    {
        public CreateModelImageCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var model = await modelRepository.GetAsync(request.ModelId, cancellationToken);
                var image = new Image(model.ModelId, request.Url, request.IsPreview);
                await imageRepository.AddAsync(image, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return image.ImageId;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
