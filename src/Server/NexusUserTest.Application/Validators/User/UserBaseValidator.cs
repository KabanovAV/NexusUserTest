using FluentValidation;
using NexusUserTest.Application.Common;
using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Application.Validators
{
    public class UserBaseValidator<T> : AbstractValidator<T> where T : UserBaseDTO
    {
        public UserBaseValidator()
        {
            When(u => u.Firstname != null, () =>
            {
                RuleFor(u => u.Firstname)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Имя"))
                    .Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Имя может содержать только буквы, пробелы и дефис");
            });

            When(u => u.Lastname != null, () =>
            {
                RuleFor(u => u.Lastname)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Имя"))
                    .Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Имя может содержать только буквы, пробелы и дефис");
            });

            RuleFor(u => u.Surname).Matches(@"^[a-zA-Zа-яА-Я\s\-]+$").WithMessage("Имя может содержать только буквы, пробелы и дефис");

            When(u => u.Login != null, () =>
            {
                RuleFor(u => u.Login)
                    .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Логин"))
                    .Length(3, 64).WithMessage(x => string.Format(ValidationMessages.LengthFromTo, "Логин", 3, 64));
            });

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(x => string.Format(ValidationMessages.Required, "Пароль"))
                .Length(8, 64).WithMessage(x => string.Format(ValidationMessages.Length, "Пароль", 8))
                .Must(u => u.Any(char.IsUpper)).WithMessage("Пароль должен содержать заглавную букву")
                .Must(u => u.Any(char.IsLower)).WithMessage("Пароль должен содержать строчную букву")
                .Must(u => u.Any(char.IsDigit)).WithMessage("Пароль должен содержать цифру")
                .Must(u => u.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage("Пароль должен содержать специальный символ");
        }
    }
}
