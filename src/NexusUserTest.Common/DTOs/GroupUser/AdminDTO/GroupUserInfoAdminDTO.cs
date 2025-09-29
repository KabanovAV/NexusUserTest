namespace NexusUserTest.Common.DTOs
{
    public class GroupUserInfoAdminDTO
    {
        public int Id { get; set; }
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Сдал 
        public UserInfoAdminDTO? User { get; set; }
        public List<ResultInfoAdminDTO>? Results { get; set; }
    }
}
