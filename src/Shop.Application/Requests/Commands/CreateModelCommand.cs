using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateModelCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brend { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class CreateModelCommandHandler : AbstractCommandHandler<CreateModelCommand, Guid>
    {
        public CreateModelCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
            var model = new Model()
            {
                Name = request.Name,
                Color = request.Color,
                Brend = request.Brend,
                SkuId = request.SkuId,
                ReleaseDate = request.ReleaseDate,
            };

            await modelRepository.AddAsync(model, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            var cratedModel = await modelRepository.FindAllAsync(x => x.Name == model.Name
                                                                 && x.Color == model.Color
                                                                 && x.Brend == model.Brend
                                                                 && x.SkuId == model.SkuId
                                                                 && x.ReleaseDate == model.ReleaseDate, cancellationToken);
            return cratedModel.First().ModelId;
        }
    }
}
