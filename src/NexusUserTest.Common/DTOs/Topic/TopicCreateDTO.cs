using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class TopicCreateDTO
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(100, ErrorMessage = "Количество символов до 100")]
        public string Title { get; set; } = string.Empty;
        public int SpecializationId { get; set; }
    }
}
