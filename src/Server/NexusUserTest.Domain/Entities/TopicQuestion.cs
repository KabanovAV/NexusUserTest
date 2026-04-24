using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class TopicQuestion : AuditableEntityBase
    {
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int QuestionId { get; set; }

        [JsonIgnore]
        public Topic? Topic { get; set; }
        [JsonIgnore]
        public Question? Question { get; set; }        
    }
}
