using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Answer : AuditableEntityBase
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }

        [JsonIgnore]
        public Question? Question { get; set; }
    }
}
