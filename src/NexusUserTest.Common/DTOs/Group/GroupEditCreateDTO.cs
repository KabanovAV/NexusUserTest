using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common.DTOs
{
    public class GroupEditCreateDTO
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(200, ErrorMessage = "Количество символов до 200")]
        public string Title { get; set; } = string.Empty;
        public int SpecializationId { get; set; }
        [Display(Name = "Начало курсов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public DateTime Begin { get; set; }
        [Display(Name = "Окончание курсов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public DateTime End { get; set; }
    }
}
