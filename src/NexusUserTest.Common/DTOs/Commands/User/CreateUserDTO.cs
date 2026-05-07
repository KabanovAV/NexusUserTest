using NexusUserTest.Common.DTOs.Bases;

namespace NexusUserTest.Common.DTOs.Commands
{
    public record CreateUserDTO(string Lastname, string Firstname, string? Surname, string Login, string Password, string? Organization, string? Position)
        : UserBaseDTO(Lastname, Firstname, Surname, Login, Password, Organization, Position);
}
