using FluentValidation;
using NexusUserTest.Common.DTOs.Commands;

namespace NexusUserTest.Application.Validators
{
    public class CreateSpecializationValidator : AbstractValidator<CreateSpecializationDTO>
    {
        public CreateSpecializationValidator()
        {
            Include(new SpecializationBaseValidator<CreateSpecializationDTO>());
        }
    }
}
