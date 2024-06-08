using Domain.Shared;

namespace Domain.Abtractions.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "A validation problem occurred.");
        Error[] Errors { get; }
    }
}
