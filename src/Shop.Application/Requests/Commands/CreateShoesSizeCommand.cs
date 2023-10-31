//using MediatR;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Base;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Commands
//{
//    public record CreateShoesSizeCommand : IRequest<Guid>
//    {
//        public Guid ShoesId { get; set; }
//        public int Size { get; set; }
//        public decimal Price { get; set; }
//        public int ItemsLeft { get; set; }
//    }

//    public class CreateShoesSizeCommandHandler : AbstractCommand, IRequestHandler<CreateShoesSizeCommand, Guid>
//    {
//        public CreateShoesSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

//        public async Task<Guid> Handle(CreateShoesSizeCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var shoesSize = new ModelSize()
//                {
//                    ShoesId = request.ShoesId,
//                    Size = request.Size,
//                    Price = request.Price,
//                    ItemsLeft = request.ItemsLeft
//                };
//                var shoesSizesRepository = unitOfWork.GetRepositoryOf<ModelSize>(true);
//                await shoesSizesRepository.AddAsync(shoesSize, cancellationToken);
//                await unitOfWork.SaveChangesAsync(cancellationToken);
//                var output = await shoesSizesRepository.FindAllAsync(x => x.ShoesId == request.ShoesId, cancellationToken);
//                return output.First().Id;
//            }
//            catch (AlreadyExistsException ex)
//            {
//                throw ex;
//            }
//            catch (NotFoundException ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
