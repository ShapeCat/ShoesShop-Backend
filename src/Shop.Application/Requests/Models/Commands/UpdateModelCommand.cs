using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.Commands
{
    public record UpdateModelCommand : IRequest<Unit>
    {
        public Guid ModelId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brend { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class UpdateModelCommandValidator : AbstractValidator<UpdateModelCommand> 
    {
        public UpdateModelCommandValidator()
        {
            RuleFor(x => x.ModelId).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Color).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Brend).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Brend).NotEmpty().MaximumLength(255);
            RuleFor(x => x.ReleaseDate).NotEmpty().LessThanOrEqualTo(DateTime.Now);
        }
    }
    public class UpdateModelCommandHandler : AbstractCommandHandler<UpdateModelCommand, Unit>
    {
        public UpdateModelCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var newModel = new Model()
                {
                    ModelId = request.ModelId,
                    Name = request.Name,
                    Color = request.Color,
                    Brend = request.Brend,
                    SkuId = request.SkuId,
                    ReleaseDate = request.ReleaseDate,
                };
                await modelRepository.EditAsync(newModel, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
