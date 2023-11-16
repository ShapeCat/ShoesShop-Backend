using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record UpdateModelSizeCommand : IRequest<Unit>
    {
        public Guid ModelSizeId { get; set; }
        public int Size { get; set; }
    }

    public class UpdateModelSizeCommandHandler : AbstractCommandHandler<UpdateModelSizeCommand, Unit>
    {
        public UpdateModelSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async override Task<Unit> Handle(UpdateModelSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelSizerepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                var newmModelSize = new ModelSize()
                {
                    Id = request.ModelSizeId,
                    Size = request.Size,
                };

                var sameModelSizes = await modelSizerepository.FindAllAsync(x => x.Size == newmModelSize.Size, cancellationToken);
                if (sameModelSizes.Any()) throw new AlreadyExistsException(newmModelSize.Size.ToString(), typeof(ModelSize));

                await modelSizerepository.EditAsync(newmModelSize, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
        }
    }
}
