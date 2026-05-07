using FluentValidation;
using NexusUserTest.Common.DTOs.Commands;

namespace NexusUserTest.Application.Validators
{
    public class UpdateSpecializationValidator : AbstractValidator<UpdateSpecializationDTO>
    {
        public UpdateSpecializationValidator()
        {
            Include(new SpecializationBaseValidator<UpdateSpecializationDTO>());
        }
    }
}
