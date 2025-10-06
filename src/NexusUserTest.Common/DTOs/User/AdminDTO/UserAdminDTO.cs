using System.ComponentModel;

namespace NexusUserTest.Common
{
    public class UserAdminDTO
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string FullName { get; set; } = string.Empty;
        [DisplayName("Логин")]
        public string Login { get; set; } = string.Empty;
        [DisplayName("Пароль")]
        public string Password { get; set; } = string.Empty;
        [DisplayName("Организация")]
        public string? Organization { get; set; }
        [DisplayName("Позиция")]
        public string? Position { get; set; }
        public List<GroupUserAdminDTO>? GroupUserItems { get; set; }
    }
}
