using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateModelSizeCommand : IRequest<Guid>
    {
        public int Size { get; set; }
    }

    public class CreateModelSizeCommandHandler : AbstractCommandHandler<CreateModelSizeCommand, Guid>
    {
        public CreateModelSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelSizeCommand request, CancellationToken cancellationToken)
        {
            var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
            var modelSize = new ModelSize()
            {
                Size = request.Size,
            };

            var allSizes = await modelSizeRepository.FindAllAsync(x => x.Size == modelSize.Size, cancellationToken);
            if (allSizes.Any())
            {
                throw new AlreadyExistsException(modelSize.Size.ToString(), typeof(ModelSize));
            }
            await modelSizeRepository.AddAsync(modelSize, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            var createdModelSize = await modelSizeRepository.FindAllAsync(x => x.Size == modelSize.Size, cancellationToken);
            return createdModelSize.First().Id;
        }
    }
}
