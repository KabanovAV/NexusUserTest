using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(200, ErrorMessage = "Количество символов от 10 до 200")]
        public string Title { get; set; } = string.Empty;
        public int SpecializationId { get; set; }
        public string SpecializationTitle { get; set; } = string.Empty;
        [Display(Name = "Начало курсов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public DateTime Begin { get; set; }
        [Display(Name = "Окончание курсов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public DateTime End { get; set; }
    }
}
