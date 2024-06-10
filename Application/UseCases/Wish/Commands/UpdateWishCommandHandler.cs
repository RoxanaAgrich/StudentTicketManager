using Domain.Abtractions.Repositories;
using Domain.Abtractions.UnitOfWork;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using static Application.Services.Wish.Command;

namespace Application.UseCases.Wish.Commands
{
    public class UpdateWishCommandHandler : IRequestHandler<UpdateWishCommand, Result>
    {
        private readonly IRepositoryBase<Domain.Entities.Wish, Guid> _wishRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateWishCommandHandler(IRepositoryBase<Domain.Entities.Wish, Guid> wishRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _wishRepository = wishRepository;
        }
        public async Task<Result> Handle(UpdateWishCommand request, CancellationToken cancellationToken)
        {
            var wish = await _wishRepository.FindByIdAsync(request.Id, cancellationToken);
            if (!wish.IsActive || wish == null)
            {
                throw new WishException.WishNotFoundException(request.Id);
            }
            wish.Name = request.name;
            _wishRepository.Update(wish);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(wish);
        }
    }
}
