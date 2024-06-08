namespace Domain.Abtractions.Shared
{
    public interface IValidationResult 
    {
        private static readonly Error ValidationError = new Error(
            "ValidationError",
            "Validation problem occurred"
            );
        Error[] Error { get; }
    }
}
