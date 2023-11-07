using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteAdressCommand : IRequest<Unit>
    {
        public Guid AdressId { get; set; }
    }

    public class DeleteAdressCommandHandler : AbstractCommandHandler, IRequestHandler<DeleteAdressCommand, Unit>
    {
        public DeleteAdressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Unit> Handle(DeleteAdressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adressRepository = unitOfWork.GetRepositoryOf<AdressVm>();
                await adressRepository.RemoveAsync(request.AdressId, cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
