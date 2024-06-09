using Domain.Abtractions.Repositories;
using Domain.Shared;
using MediatR;
using static Application.Services.V1.Wish.Command;

namespace Application.UseCases.V1.Wish.Commands;

public class CreateWishCommandHandler : IRequestHandler<CreateWishCommand, Result>
{
    private readonly IRepositoryBase<Domain.Entities.Wish, Guid> _wishRepository;
    public CreateWishCommandHandler(IRepositoryBase<Domain.Entities.Wish, Guid> wishRepository)
    {
        _wishRepository = wishRepository;
    }
    public async Task<Result> Handle(CreateWishCommand request, CancellationToken cancellationToken)
    {
        var wish = new Domain.Entities.Wish(Guid.NewGuid(), request.name);
        _wishRepository.Add(wish);
        return Result.Success();
    }
}
