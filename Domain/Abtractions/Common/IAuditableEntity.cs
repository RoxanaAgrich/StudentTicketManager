namespace Domain.Abtractions.Common
{
    public interface IAuditableEntity
    {
        DateTimeOffset CreatedOnUtc { get; set; }
        DateTimeOffset? ModifiedOnUtc { get; set; }
    }
}
