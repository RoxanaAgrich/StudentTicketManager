using Domain.Shared;
using MediatR;
using System.Windows.Input;

namespace Application.Services.Wish;

public static class Command
{
    public record CreateWishCommand(string name) : IRequest<Result>;

    public record UpdateWishCommand(Guid Id, string name) : IRequest<Result>;

    public record DeleteWishCommand(Guid Id) : IRequest<Result>;
}
