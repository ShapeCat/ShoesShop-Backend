using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateModelVariantCommand : IRequest<Guid>
    {
        public Guid ModelId { get; set; }
        public Guid ModelSizeId { get; set; }
        public int ItemsLeft { get; set; }
    }

    public class CreateModelVariantCommandHandler : AbstractCommandHandler<CreateModelVariantCommand, Guid>
    {
        public CreateModelVariantCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelVariantCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();

                var model = await modelRepository.GetAsync(request.ModelId, cancellationToken);
                var modelSize = await modelSizeRepository.GetAsync(request.ModelSizeId, cancellationToken);
                var modelVariant = new ModelVariant()
                {
                    Model = model,
                    ModelSize = modelSize,
                    ItemsLeft = request.ItemsLeft,
                };
                await modelVariantRepository.AddAsync(modelVariant, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                var createdModelVariant = await modelVariantRepository.FindAllAsync(x => x.Model == modelVariant.Model
                                                                                   && x.ModelSize == modelVariant.ModelSize
                                                                                   && x.ItemsLeft == request.ItemsLeft, cancellationToken);
                return createdModelVariant.First().Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
