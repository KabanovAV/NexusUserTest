using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Display(Name = "Количество вопросов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public int CountOfQuestion { get; set; }
        [Display(Name = "Время на прохождение теста")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public TimeSpan Timer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
    }
}
