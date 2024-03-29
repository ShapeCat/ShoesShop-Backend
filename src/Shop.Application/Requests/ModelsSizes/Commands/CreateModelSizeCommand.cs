﻿using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsSizes.Commands
{
    public record CreateModelSizeCommand : IRequest<Guid>
    {
        public int Size { get; set; }
    }

    public class CreateModelSizeCommandValidator : AbstractValidator<CreateModelSizeCommand>
    {
        public CreateModelSizeCommandValidator()
        {
            RuleFor(x => x.Size).GreaterThan(0).LessThan(int.MaxValue);
        }
    }

    public class CreateModelSizeCommandHandler : AbstractCommandHandler<CreateModelSizeCommand, Guid>
    {
        public CreateModelSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelSizeCommand request, CancellationToken cancellationToken)
        {
            var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
            var allSizes = await modelSizeRepository.FindAllAsync(x => x.Size == request.Size, cancellationToken);
            if (allSizes.Any()) throw new AlreadyExistsException(request.Size.ToString(), typeof(ModelSize));
            var modelSize = new ModelSize(request.Size);
            await modelSizeRepository.AddAsync(modelSize, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return modelSize.ModelSizeId;
        }
    }
}
