//using MediatR;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Commands
//{
//    public record DeleteDescriptionCommand : IRequest<Unit>
//    {
//        public Guid DescriptionId { get; set; }
//    }

//    public class DeleteDescriptionCommandHandler : AbstractCommand, IRequestHandler<DeleteDescriptionCommand, Unit>
//    {
//        public DeleteDescriptionCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

//        public async Task<Unit> Handle(DeleteDescriptionCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var descriptionRepository = unitOfWork.GetRepositoryOf<Description>(true); 
//                await descriptionRepository.RemoveAsync(request.DescriptionId, cancellationToken);
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
