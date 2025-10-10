using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class UserAdminDTO
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(90, ErrorMessage = "Количество символов до 90")]
        public string FullName { get; set; } = string.Empty;
        [DisplayName("Логин")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Login { get; set; } = string.Empty;
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Password { get; set; } = string.Empty;
        [DisplayName("Организация")]
        public string? Organization { get; set; }
        [DisplayName("Позиция")]
        public string? Position { get; set; }
        public List<GroupUserAdminDTO>? GroupUserItems { get; set; }
    }
}
