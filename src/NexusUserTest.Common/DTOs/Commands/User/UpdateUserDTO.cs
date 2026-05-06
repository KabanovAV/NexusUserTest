namespace NexusUserTest.Common.DTOs
{
    public record UpdateUserDTO(int Id, string Lastname, string Firstname, string? Surname, string Login, string Password, string? Organization, string? Position)
        : UserBaseDTO(Lastname, Firstname, Surname, Login, Password, Organization, Position);
}
