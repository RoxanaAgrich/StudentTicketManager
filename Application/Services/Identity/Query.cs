using Domain.Shared;
using MediatR;

namespace Application.Services.Identity;

public static class Query
{
    public record Login(string Email, string Password) : IRequest<Result<Response.Authenticated>>;

    public record Token(string? AccessToken, string? RefreshToken) : IRequest<Result<Response.Authenticated>>;
}