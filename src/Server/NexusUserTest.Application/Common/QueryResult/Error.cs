using FluentValidation.Results;

namespace NexusUserTest.Application.Common
{
    public record Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        public Dictionary<string, string[]>? ValidationErrors { get; }

        public Error(string code, string description, ErrorType type, Dictionary<string, string[]>? validationErrors = null)
        {
            Code = code;
            Description = description;
            Type = type;
            ValidationErrors = validationErrors;
        }

        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new("General.Null", "Было предоставлено нулевое значение.", ErrorType.Failure);

        public static Error Failure(string code, string description)
            => new(code, description, ErrorType.Failure);

        public static Error NotFound(string code, string description)
            => new(code, description, ErrorType.NotFound);

        public static Error Problem(string code, string description)
            => new(code, description, ErrorType.Problem);

        public static Error Conflict(string code, string description)
            => new(code, description, ErrorType.Conflict);

        public static Error Validation(string code, string description, ValidationResult validationResult)
        {
            var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return new(code, description, ErrorType.Validation, errors);
        }
    }
}
