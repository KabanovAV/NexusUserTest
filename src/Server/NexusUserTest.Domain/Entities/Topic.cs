using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Topic : AuditableEntityBase
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int SpecializationId { get; set; }

        [JsonIgnore]
        public Specialization? Specialization { get; set; }
        [JsonIgnore]
        public List<TopicQuestion>? TopicQuestions { get; set; }
    }
}
