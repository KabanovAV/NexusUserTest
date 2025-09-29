namespace NexusUserTest.Common.DTOs
{
    public class UserInfoAdminDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string? Organization { get; set; }
    }
}
