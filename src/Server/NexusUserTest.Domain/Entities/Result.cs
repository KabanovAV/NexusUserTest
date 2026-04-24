using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Result : AuditableEntityBase
    {
        [Required]
        public int GroupUserId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }

        [JsonIgnore]
        public GroupUser? GroupUser { get; set; }
        [JsonIgnore]
        public Question? Question { get; set; }
        [JsonIgnore]
        public Answer? Answer { get; set; }
    }
}
