//using MediatR;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Commands
//{
//    public record UpdateDescriptionCommand : IRequest<Unit>
//    {
//        public Guid DescriptionId { get; set; }
//        public string ColorName { get; set; }
//        public string SkuID { get; set; }
//        public DateTime ReleaseDate { get; set; }
//    }

//    public class UpdateDescriptionCommandHandler : AbstractCommand, IRequestHandler<UpdateDescriptionCommand, Unit>
//    {
//        public UpdateDescriptionCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

//        public async Task<Unit> Handle(UpdateDescriptionCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var newDescription = new Description()
//                {
//                    Id = request.DescriptionId,
//                    ColorName = request.ColorName,
//                    SkuID = request.SkuID,
//                    ReleaseDate = request.ReleaseDate,
//                };
//                var descriptionRepository = unitOfWork.GetRepositoryOf<Description>(true);
//                await descriptionRepository.EditAsync(newDescription, cancellationToken);
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
