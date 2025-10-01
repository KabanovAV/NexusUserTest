namespace NexusUserTest.Common
{
    public class UserInfoTestDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public List<GroupUserInfoTestDTO>? GroupUsers { get; set; }
    }
}
