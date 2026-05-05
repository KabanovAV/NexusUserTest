using FluentValidation;
using NexusUserTest.Application.Common;
using NexusUserTest.Common.DTOs;

namespace NexusUserTest.Application.Validators
{
    public class GroupBaseValidator<T> : AbstractValidator<T> where T : GroupBaseDTO
    {
        public GroupBaseValidator()
        {
            When(g => g.Title != null, () =>
            {
                RuleFor(g => g.Title)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Название"))
                    .Length(1, 200).WithMessage(x => string.Format(ValidationMessages.LengthFromTo, "Название", 1, 200));
            });

            RuleFor(g => g.Begin)
                .GreaterThan(DateTime.MinValue).WithMessage(x => string.Format(ValidationMessages.Required, "Начало курсов"));

            When(g => g.Begin > DateTime.MinValue, () =>
            {
                RuleFor(g => g.End)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Окончание курсов"))
                    .GreaterThan(x => x.Begin).WithMessage(x => string.Format(ValidationMessages.GreaterThan, "Окончание курсов", "Начало курсов"));
            });
        }
    }
}
