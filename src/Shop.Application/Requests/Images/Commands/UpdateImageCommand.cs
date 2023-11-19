﻿using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Images.Commands
{
    public record UpdateImageCommand : IRequest<Unit>
    {
        public Guid ImageId { get; set; }
        public string Url { get; set; }
        public bool IsPreview { get; set; }
    }

    public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
    {
        public UpdateImageCommandValidator()
        {
            RuleFor(x => x.ImageId).NotEqual(Guid.Empty);
            RuleFor(x => x.Url).NotEmpty().MaximumLength(256);
        }
    }
    public class UpdateImageCommandhandler : AbstractCommandHandler<UpdateImageCommand, Unit>
    {
        public UpdateImageCommandhandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
                var newImage = new Image()
                {
                    ImageId = request.ImageId,
                    Url = request.Url,
                    IsPreview = request.IsPreview,
                };
                await imageRepository.EditAsync(newImage, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
