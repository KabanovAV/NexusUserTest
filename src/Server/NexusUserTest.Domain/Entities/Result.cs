using NexusUserTest.Domain.Common;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Result : AuditableEntityBase
    {
        public int GroupUserId { get; set; }
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
