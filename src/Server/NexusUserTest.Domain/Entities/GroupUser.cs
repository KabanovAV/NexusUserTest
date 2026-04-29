using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class GroupUser : AuditableEntityBase
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Пройден
        public DateTime? EndTest { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public List<TestResult>? Results { get; set; }
    }
}
