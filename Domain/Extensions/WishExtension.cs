namespace Domain.Extensions;

public static class WishExtension
{
    public static string GetSortWishProperty(string sortColumn)
        => sortColumn.ToLower() switch
        {
            "name" => "Name",
            _ => "Id"
        };
}