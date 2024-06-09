namespace Application.Services.V1.Wish;

public static class Response
{
    public record WishResponse(Guid Id, string Name, decimal Price, string Description);
}
