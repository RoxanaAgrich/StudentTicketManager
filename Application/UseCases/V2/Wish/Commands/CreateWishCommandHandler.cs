using Domain.Abtractions.Repositories;
using Domain.Abtractions.UnitOfWork;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using static Application.Services.V2.Wish.Command;

namespace Application.UseCases.V2.Wish.Commands;

public class CreateWishCommandHandler : IRequestHandler<CreateWishCommand, Result>
{
    private readonly IUnitOfWorkMongboDb _unitOfWork;
    public CreateWishCommandHandler(IUnitOfWorkMongboDb unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(CreateWishCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.StartSessionAsync(cancellationToken);
        var wishRepository = _unitOfWork.GetRepository<Domain.Entities.Wish, Guid>();
        var wish = new Domain.Entities.Wish(Guid.NewGuid(), request.name);
        wishRepository.Add(wish);
        await _unitOfWork.CommitAsync(cancellationToken);
        _unitOfWork.Dispose();
        return Result.Success();
    }
}
