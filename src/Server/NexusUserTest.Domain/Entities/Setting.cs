using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Setting : AuditableEntityBase
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int CountOfQuestion { get; set; }
        [Required]
        public TimeSpan Timer { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
    }
}
