using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.Commands
{
    public record CreateModelCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
    {
        public CreateModelCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Color).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SkuId).NotEmpty().MaximumLength(255);
            RuleFor(x => x.ReleaseDate).NotEmpty().LessThanOrEqualTo(DateTime.Now);
        }
    }

    public class CreateModelCommandHandler : AbstractCommandHandler<CreateModelCommand, Guid>
    {
        public CreateModelCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
            var model = new Model()
            {
                ModelId = Guid.NewGuid(),
                Name = request.Name,
                Color = request.Color,
                Brand = request.Brand,
                SkuId = request.SkuId,
                ReleaseDate = request.ReleaseDate,
            };
            await modelRepository.AddAsync(model, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return model.ModelId;
        }
    }
}
