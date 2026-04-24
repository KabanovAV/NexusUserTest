using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class User : AuditableEntityBase
    {
        [Required, StringLength(30)]
        public string LastName { get; set; } = string.Empty;
        [Required, StringLength(30)]
        public string FirstName { get; set; } = string.Empty;
        public string? Surname { get; set; }
        [Required]
        public string Login { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? Organization { get; set; }
        public string? Position { get; set; }

        [JsonIgnore]
        public List<GroupUser>? GroupUser { get; set; }
    }
}
