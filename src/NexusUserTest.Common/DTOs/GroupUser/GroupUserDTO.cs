namespace NexusUserTest.Common.DTOs
{
    public class GroupUserDTO
    {
        public int Id { get; set; }
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Провалил; 4 Сдал 
        public UserInfoDTO? User { get; set; }
        //public List<ResultDTO>? Results { get; set; }
    }
}
