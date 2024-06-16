using Domain.Abtractions.UnitOfWork;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using static Application.Services.V1.Wish.Command;

namespace Application.UseCases.V2.Wish.Commands
{
    public class UpdateWishCommandHandler : IRequestHandler<UpdateWishCommand, Result>
    {
        private readonly IUnitOfWorkMongboDb _unitOfWork;
        public UpdateWishCommandHandler(IUnitOfWorkMongboDb unitOfWork)
        => _unitOfWork = unitOfWork; 
        public async Task<Result> Handle(UpdateWishCommand request, CancellationToken cancellationToken)
        {
            var wishRepository = _unitOfWork.GetRepository<Domain.Entities.Wish, Guid>();
            var wish = await wishRepository.FindByIdAsync(request.Id);
            if(wish== null || wish.IsActive == false)
                 throw new WishException.WishNotFoundException(request.Id);
            wish.Name = request.name;
            return Result.Success(wish);

        }
    }
}
