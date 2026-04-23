using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Answer : AuditableEntityBase
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Title { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        [Display(Name = "Верный ответ")]
        public bool IsCorrect { get; set; }

        [JsonIgnore]
        public Question? Question { get; set; }
    }
}
