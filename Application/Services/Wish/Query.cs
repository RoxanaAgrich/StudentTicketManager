﻿using Domain.Enumerations;
using Domain.Shared;
using MediatR;
using static Application.Services.Wish.Response;

namespace Application.Services.Wish;

public static class Query
{
    public record GetWishesQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IRequest<Result<PagedResult<WishResponse>>>;
    public record GetWishByIdQuery(Guid Id) : IRequest<Result<WishResponse>>;
}