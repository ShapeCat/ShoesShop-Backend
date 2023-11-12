﻿using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record UpdateImageCommand : IRequest<Unit>
    {
        public Guid ImageId { get; set; }
        public string Url { get; set; }
        public bool IsPreview { get; set; }
    }

    public class UpdateImageCommandhandler : AbstractCommandHandler<UpdateImageCommand, Unit>
    {
        public UpdateImageCommandhandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = unitOfWork.GetRepositoryOf<Image>(true);
                var newImage = new Image()
                {
                    Id = request.ImageId,
                    Url = request.Url,
                    IsPreview = request.IsPreview,
                };
                await imageRepository.EditAsync(newImage, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}