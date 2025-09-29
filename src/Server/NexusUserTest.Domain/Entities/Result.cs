using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public int GroupUserId { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }

        [JsonIgnore]
        public GroupUser? GroupUser { get; set; }
        [JsonIgnore]
        public Question? Question { get; set; }
        [JsonIgnore]
        public Answer? Answer { get; set; }
    }
}
