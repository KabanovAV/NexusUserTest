namespace NexusUserTest.Common.DTOs
{
    public class GroupUserDTO
    {
        public int Id { get; set; }
        public int Status { get; set; } // 1 Недопущен; 2 Допущен; 3 Пройден
        public UserDTO? User { get; set; }
        //public List<ResultDTO>? Results { get; set; }
    }
}
