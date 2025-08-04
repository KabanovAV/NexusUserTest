using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common.DTOs
{
    public class TopicDTO
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(100, ErrorMessage = "Количество символов до 100")]
        public string Title { get; set; } = string.Empty;
        public int SpecializationId { get; set; }
        public string SpecializationTitle { get; set; } = string.Empty;
    }
}
