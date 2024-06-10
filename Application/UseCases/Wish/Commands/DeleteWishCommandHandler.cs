using Domain.Abtractions.Repositories;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using static Application.Services.Wish.Command;

namespace Application.UseCases.Wish.Commands
{
    public class DeleteWishCommandHandler : IRequestHandler<DeleteWishCommand, Result>
    {
        private readonly IRepositoryBase<Domain.Entities.Wish, Guid> _wishRepository;
        public DeleteWishCommandHandler(IRepositoryBase<Domain.Entities.Wish, Guid> wishRepository)
        {
            _wishRepository = wishRepository;
        }
        public async Task<Result> Handle(DeleteWishCommand request, CancellationToken cancellationToken)
        {
            var wish = await _wishRepository.FindByIdAsync(request.Id, cancellationToken);
            if (!wish.IsActive || wish == null)
            {
                throw new WishException.WishNotFoundException(request.Id);
            }
            wish.IsActive = false;
            return Result.Success();
        }
    }
}
