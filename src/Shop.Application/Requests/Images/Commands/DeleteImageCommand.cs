﻿using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Images.Commands
{
    public record DeleteImageCommand : IRequest<Unit>
    {
        public Guid ImageId { get; set; }
    }

    public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(x => x.ImageId).NotEqual(Guid.Empty);
        }
    }

    public class DeleteImageCommandHandler : AbstractCommandHandler<DeleteImageCommand, Unit>
    {
        public DeleteImageCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = UnitOfWork.GetRepositoryOf<Image>();
                await imageRepository.RemoveAsync(request.ImageId, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
