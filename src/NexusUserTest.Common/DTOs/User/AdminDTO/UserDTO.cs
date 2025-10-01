using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(90, ErrorMessage = "Количество символов до 90")]
        public string FullName { get; set; } = string.Empty;
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Login { get; set; } = string.Empty;
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "Организация")]
        public string? Organization { get; set; }
        [Display(Name = "Позиция")]
        public string? Position { get; set; }
        public List<GroupUserCreateDTO>? GroupUserItems { get; set; }
    }
}
