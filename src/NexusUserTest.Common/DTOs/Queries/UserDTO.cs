namespace NexusUserTest.Common.DTOs
{
    public record UserDTO(int Id, string FullName, string Login, string Password, string? Organization, string? Position);
}
