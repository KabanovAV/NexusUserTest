using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(30, ErrorMessage = "Количество символов до 30")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(30, ErrorMessage = "Количество символов до 30")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Отчество")]
        public string? Surname { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Login { get; set; } = string.Empty;
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public string Password { get; set; } = string.Empty;
        public string? Organization { get; set; }
        public string? Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }

        [JsonIgnore]
        public List<GroupUser>? GroupUser { get; set; }
    }
}
