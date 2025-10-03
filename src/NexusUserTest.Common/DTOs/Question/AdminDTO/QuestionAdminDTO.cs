using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class QuestionAdminDTO
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Title { get; set; } = string.Empty;
        public List<AnswerAdminDTO>? AnswerItems { get; set; }
        public List<TopicQuestionCreateDTO>? TopicQuestionItems { get; set; }
    }
}
