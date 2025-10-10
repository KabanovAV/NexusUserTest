using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NexusUserTest.Common
{
    public class LoginDto()
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [DisplayName("Логин")]
        public string? UserLogin { get; set; }
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [DisplayName("Пароль")]
        public string? Password { get; set; }
    }
}
