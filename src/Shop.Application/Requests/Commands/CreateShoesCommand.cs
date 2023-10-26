using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateShoesCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }

    public class CreateShoesCommandHandler : AbstractCommand, IRequestHandler<CreateShoesCommand, Guid>
    {
        public CreateShoesCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Guid> Handle(CreateShoesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoes = new Shoes()
                {
                    Name = request.Name
                };
                var shoesRepository = unitOfWork.GetRepositoryOf<Shoes>(true);
                await shoesRepository.AddAsync(shoes, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                var output = await shoesRepository.FindAllAsync(x => x.Name == request.Name, cancellationToken);
                return output.First().Id;
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
        }
    }
}
