using FluentValidation;
using NexusUserTest.Application.Common;
using NexusUserTest.Common;

namespace NexusUserTest.Application.Validators
{
    public class SpecializationBaseValidator<T> : AbstractValidator<T> where T : SpecializationDTO
    {
        public SpecializationBaseValidator()
        {
            When(s => s.Title != null, () =>
            {
                RuleFor(s => s.Title)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Название"))
                    .Length(100).WithMessage(x => string.Format(ValidationMessages.Length, "Название", 100));
            });
        }
    }
}
