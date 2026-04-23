using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Question : AuditableEntityBase
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Title { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Answer>? Answers { get; set; }
        [JsonIgnore]
        public List<TopicQuestion>? TopicQuestion { get; set; }
    }
}
