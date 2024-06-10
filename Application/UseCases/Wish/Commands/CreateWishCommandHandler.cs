using Domain.Abtractions.Repositories;
using Domain.Shared;
using MediatR;
using static Application.Services.Wish.Command;

namespace Application.UseCases.Wish.Commands
{
    public class CreateWishCommandHandler : IRequestHandler<CreateWishCommand, Result>
    {
        private readonly IRepositoryBase<Domain.Entities.Wish, Guid> _wishRepository;
        public CreateWishCommandHandler(IRepositoryBase<Domain.Entities.Wish, Guid> wishRepository)
        {
            _wishRepository = wishRepository;
        }
        public async Task<Result> Handle(CreateWishCommand request, CancellationToken cancellationToken)
        {
            // var wish = new Domain.Entities.Wish(Guid.NewGuid(), request.name, true);
            var wish = new Domain.Entities.Wish(Guid.NewGuid(), request.name);
            wish.IsActive = true;
            _wishRepository.Add(wish);
            return Result.Success();
        }
    }
}
