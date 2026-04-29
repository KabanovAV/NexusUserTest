using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Group : AuditableEntityBase
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int SpecializationId { get; set; }
        [Required]
        public DateTime Begin { get; set; }
        [Required]
        public DateTime End { get; set; }

        [JsonIgnore]
        public Specialization? Specialization { get; set; }
        [JsonIgnore]
        public List<GroupUser>? GroupUser { get; set; }
        [JsonIgnore]
        public TestSetting? Setting { get; set; }
    }
}
