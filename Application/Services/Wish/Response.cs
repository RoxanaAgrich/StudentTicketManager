namespace Application.Services.Wish;

public static class Response
{
    public record WishResponse(Guid Id, string Name);
}
