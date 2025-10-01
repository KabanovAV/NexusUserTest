using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class QuestionCreateDTO
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Title { get; set; } = string.Empty;
        public List<AnswerDTO>? AnswerItems { get; set; }
        public List<TopicQuestionCreateDTO>? TopicQuestionItems { get; set; }
    }
}
