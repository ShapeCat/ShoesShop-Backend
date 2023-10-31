//using MediatR;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Commands
//{
//    public record DeleteShoesCommand : IRequest<Unit>
//    {
//        public Guid ShoesId { get; set; }
//    }

//    public class DeleteShoesCommandHandler : AbstractCommand, IRequestHandler<DeleteShoesCommand, Unit>
//    {
//        public DeleteShoesCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

//        public async Task<Unit> Handle(DeleteShoesCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var shoesRepository = unitOfWork.GetRepositoryOf<Model>(true);
//                await shoesRepository.RemoveAsync(request.ShoesId, cancellationToken);
//                await unitOfWork.SaveChangesAsync(cancellationToken);
//                return Unit.Value;
//            }
//            catch (NotFoundException ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
