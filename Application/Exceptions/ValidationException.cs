using Domain.Exceptions;

namespace Application.Exceptions
{
    public sealed class ValidationException : DomainException
    {
        public ValidationException(IReadOnlyCollection<ValidationError> errors)
            : base("Validation Failure", "One or more validattion errors occures")
            => Errors = errors;
        public IReadOnlyCollection<ValidationError> Errors { get; }
    }
    public record ValidationError(string PropertyName, string ErrorMessage);
}
