//using MediatR;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Commands
//{
//    public record UpdateShoesCommand : IRequest<Unit>
//    {
//        public Guid ShoesId { get; set; }
//        public string Name { get; set; }
//    }

//    public class UpdateShoesNameCommandHandler : AbstractCommand, IRequestHandler<UpdateShoesCommand, Unit>
//    {
//        public UpdateShoesNameCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

//        public async Task<Unit> Handle(UpdateShoesCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var newShoes = new Model()
//                {
//                    Id = request.ShoesId,
//                    Name = request.Name,
//                };
//                var shoesRepository = unitOfWork.GetRepositoryOf<Model>(true);
//                await shoesRepository.EditAsync(newShoes, cancellationToken);
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
