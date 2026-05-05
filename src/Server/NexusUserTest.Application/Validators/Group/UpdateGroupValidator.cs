using FluentValidation;
using NexusUserTest.Application.Common;
using NexusUserTest.Common.DTOs;

namespace NexusUserTest.Application.Validators
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupDTO>
    {
        public UpdateGroupValidator()
        {
            Include(new GroupBaseValidator<UpdateGroupDTO>());

            RuleFor(g => g.SpecializationId)
                .GreaterThan(0).WithMessage(x => string.Format(ValidationMessages.Required, "Специализации"));
        }
    }
}
