using FluentValidation;
using NexusUserTest.Application.Common;
using NexusUserTest.Common.DTOs.Commands;

namespace NexusUserTest.Application.Validators
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupDTO>
    {
        public CreateGroupValidator()
        {
            Include(new GroupBaseValidator<CreateGroupDTO>());

            RuleFor(g => g.SpecializationId)
                .GreaterThan(0).WithMessage(x => string.Format(ValidationMessages.Required, "Специализации"));
        }
    }
}
