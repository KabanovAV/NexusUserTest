using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class AnswerAdminDTO
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Title { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; } = string.Empty;
        [Display(Name = "Верный ответ")]
        public bool IsCorrect { get; set; }
    }
}
