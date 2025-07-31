using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class TopicQuestion
    {
        [Key]
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }

        [JsonIgnore]
        public Topic? Topic { get; set; }
        [JsonIgnore]
        public Question? Question { get; set; }        
    }
}
