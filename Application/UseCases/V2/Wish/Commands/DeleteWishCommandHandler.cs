using Domain.Abtractions.UnitOfWork;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using static Application.Services.V2.Wish.Command;

namespace Application.UseCases.V2.Wish.Commands
{
    public class DeleteWishCommandHandler : IRequestHandler<DeleteWishCommand, Result>
    {
        private readonly IUnitOfWorkMongboDb _unitOfWork;
        public DeleteWishCommandHandler(IUnitOfWorkMongboDb unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteWishCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Domain.Entities.Wish, Guid>();
            var wish = await repository.FindByIdAsync(request.Id);
            if (wish == null|| wish.IsActive == false)
            {
                throw new WishException.WishNotFoundException(request.Id);
            }
            wish.IsActive = false;
            return Result.Success();
        }
    }
}
