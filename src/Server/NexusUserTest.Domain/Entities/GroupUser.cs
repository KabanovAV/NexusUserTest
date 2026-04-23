using NexusUserTest.Domain.Common;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class GroupUser : AuditableEntityBase
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Пройден
        public DateTime? EndTest { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public List<Result>? Results { get; set; }
    }
}
