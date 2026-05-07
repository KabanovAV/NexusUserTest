using FluentValidation;
using NexusUserTest.Common.DTOs.Commands;

namespace NexusUserTest.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            Include(new UserBaseValidator<UpdateUserDTO>());
        }
    }
}
