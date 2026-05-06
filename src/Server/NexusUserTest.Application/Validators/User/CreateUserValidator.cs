using FluentValidation;
using NexusUserTest.Common.DTOs;

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
