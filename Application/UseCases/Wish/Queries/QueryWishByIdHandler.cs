using AutoMapper;
using Domain.Abtractions.Repositories;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;
using static Application.Services.Wish.Query;
using static Application.Services.Wish.Response;

namespace Application.UseCases.Wish.Queries
{
    public class QueryWishByIdHandler : IRequestHandler<GetWishByIdQuery,Result<WishResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Wish, Guid> _wishRepository;
        private readonly IMapper _mapper;
        public QueryWishByIdHandler(IRepositoryBase<Domain.Entities.Wish, Guid> wishRepository,
            IMapper mapper)
        {
            _wishRepository = wishRepository;
            _mapper = mapper;
        }

        public async Task<Result<WishResponse>> Handle(GetWishByIdQuery request, CancellationToken cancellationToken)
        {
            var wish = await _wishRepository.FindByIdAsync(request.Id, cancellationToken);
            if (wish == null || wish.IsActive == false) 
                throw new WishException.WishNotFoundException(request.Id);
            var result = _mapper.Map<WishResponse>(wish);
            return result;
        }
    }
}
