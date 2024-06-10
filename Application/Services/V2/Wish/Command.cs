using Domain.Shared;
using MediatR;

namespace Application.Services.V2.Wish;

public static class Command
{
    public record CreateWishCommand(string name) : IRequest<Result>;

    public record UpdateWishCommand(Guid Id, string name) : IRequest<Result>;

    public record DeleteWishCommand(Guid Id) : IRequest<Result>;
}
