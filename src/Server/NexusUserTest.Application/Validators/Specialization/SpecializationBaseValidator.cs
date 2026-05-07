using FluentValidation;
using NexusUserTest.Application.Common;
using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Application.Validators
{
    public class SpecializationBaseValidator<T> : AbstractValidator<T> where T : SpecializationBaseDTO
    {
        public SpecializationBaseValidator()
        {
            When(s => s.Title != null, () =>
            {
                RuleFor(s => s.Title)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Название"))
                    .Length(1, 100).WithMessage(x => string.Format(ValidationMessages.LengthFromTo, "Название", 1, 100));
            });
        }
    }
}
