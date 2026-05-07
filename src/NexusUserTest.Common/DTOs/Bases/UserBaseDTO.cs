namespace NexusUserTest.Common.DTOs.Bases
{
    public record UserBaseDTO(string Lastname, string Firstname, string? Surname, string Login, string Password, string? Organization, string? Position);
}
