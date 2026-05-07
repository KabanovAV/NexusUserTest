using FluentValidation;
using NexusUserTest.Common.DTOs.Commands;

namespace NexusUserTest.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            Include(new UserBaseValidator<CreateUserDTO>());
        }
    }
}
