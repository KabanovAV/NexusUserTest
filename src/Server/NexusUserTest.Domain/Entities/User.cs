using NexusUserTest.Domain.Common;

namespace NexusUserTest.Domain.Entities
{
    public class User : AuditableEntityBase
    {
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Surname { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Organization { get; set; }
        public string? Position { get; set; }
        public List<GroupUser>? GroupUsers { get; set; }
    }
}
