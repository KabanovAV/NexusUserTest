using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common.DTOs
{
    public class SettingCreateDTO
    {
        public int GroupId { get; set; }
        [Display(Name = "Количество вопросов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public int CountOfQuestion { get; set; }
        [Display(Name = "Время на прохождение теста")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public TimeSpan Timer { get; set; }
    }
}
