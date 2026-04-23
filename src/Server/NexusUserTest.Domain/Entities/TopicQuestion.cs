using NexusUserTest.Domain.Common;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class TopicQuestion : AuditableEntityBase
    {
        public int TopicId { get; set; }
        public int QuestionId { get; set; }

        [JsonIgnore]
        public Topic? Topic { get; set; }
        [JsonIgnore]
        public Question? Question { get; set; }        
    }
}
