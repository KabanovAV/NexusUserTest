using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Setting : AuditableEntityBase
    {
        public int GroupId { get; set; }
        [Display(Name = "Количество вопросов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public int CountOfQuestion { get; set; }
        [Display(Name = "Время на прохождение теста")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public TimeSpan Timer { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
    }
}
