using Domain.Abtractions.Repositories;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using static Application.Services.Wish.Command;

namespace Application.UseCases.Wish.Commands
{
    internal class UpdateWishCommandHandler : IRequestHandler<UpdateWishCommand, Result>
    {
        private readonly IRepositoryBase<Domain.Entities.Wish, Guid> _wishRespository;
        public UpdateWishCommandHandler(IRepositoryBase<Domain.Entities.Wish, Guid> wishRespository)
        {
            _wishRespository = wishRespository;
        }

        public async Task<Result> Handle(UpdateWishCommand request, CancellationToken cancellationToken)
        {
           var wishUpdate = await _wishRespository.FindByIdAsync(request.Id,cancellationToken);
            if(wishUpdate == null|| wishUpdate.IsActive == false) 
                throw new WishException.WishNotFoundException(request.Id);
            wishUpdate.Name = request.name;
            _wishRespository.Update(wishUpdate);
            return Result.Success();
        }
    }
}
